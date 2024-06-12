using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Hero")]
    public static HeroStats.HeroType selectedHero;
    [NonSerialized] public HeroStats hero;


    [Header("UI")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] XpBar xpBar;
    [SerializeField] AimCrosshair aimCrosshair;
    [SerializeField] UpgradesUI upgradeUI;

    [Header("Spawners")]
    [SerializeField] GameObject spawnersParent;
    [SerializeField] GameObject spawnerPrefab;

    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    Camera cam;
    Vector2 movement, aimPosition;
    bool shoots = false, autoAim = false;

    // Private variables
    float ivincibleTime = 0;
    float gainedXp = 0;

    void Start()
    {
        hero = HeroStats.Heros[selectedHero];

        // Initialization
        cam = Camera.main;

        // Set initial values
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = hero.sprite;
        healthBar.maxHealth = hero.startHp;
        healthBar.hp = hero.startHp;
        xpBar.xpToNextLevel = 50;
        xpBar.xp = 0;

        // Add spawners
        AddGun();
    }
    void Update()
    {
        rb.AddForce(Vector2.zero);
        // Movement
        if (Input.GetKeyDown(KeyCode.Space))
        {
            autoAim = !autoAim;
            aimCrosshair.SetVisable(autoAim);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shoots = !shoots;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shoots = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            shoots = false;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (autoAim)
        {
            float closestDistance = Mathf.Infinity;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float distance = Vector2.Distance(enemy.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    aimPosition = enemy.transform.position;
                }
            }
        }
        else
        {
            aimPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // Invincible time
        if (ivincibleTime > 0)
        {
            ivincibleTime -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        // Movement
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.MovePosition(rb.position + movement.normalized * hero.moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = aimPosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        spawnersParent.transform.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Update spawners
        foreach (Transform child in spawnersParent.transform)
        {
            BulletSpawner spawner = child.GetComponent<BulletSpawner>();
            spawner.bulletStats = hero.bulletStats;
            spawner.fireRate = hero.fireRate;
            spawner.shoot = shoots;
        }

        // Update aim crosshair
        aimCrosshair.SetPosition(aimPosition);
        CheckIfLevelUp();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Hit(enemy.touchDamage);
        }
        if (collision.CompareTag("XP"))
        {
            XpPoint xpPoint = collision.GetComponent<XpPoint>();
            AddXP(xpPoint.xpValue);
            Destroy(collision.gameObject);
        }

    }
    public void AddGun()
    {
        GameObject spawner = Instantiate(spawnerPrefab, spawnersParent.transform.position, spawnersParent.transform.rotation);
        spawner.GetComponent<BulletSpawner>().bulletPrefab = hero.bulletPrefab;
        spawner.transform.parent = spawnersParent.transform;

        float numberOfSpawners = spawnersParent.transform.childCount - 1;
        float start = -numberOfSpawners * hero.bulletSpread / 2;
        int i = 0;
        spawnersParent.transform.rotation = Quaternion.Euler(0, 0, 0);
        for (float angle = start; angle <= -start; angle += hero.bulletSpread)
        {
            spawnersParent.transform.GetChild(i).rotation = Quaternion.Euler(0, 0, angle);
            i++;
        }
    }
    public void AddMaxHp(float addedHp)
    {
        healthBar.maxHealth += addedHp;
        healthBar.hp += addedHp;
    }
    public void Heal(float heal)
    {
        healthBar.hp += heal;
    }
    public void Hit(float damage)
    {
        if (ivincibleTime > 0)
        {
            return;
        }
        healthBar.hp -= damage;
        ivincibleTime = 1;
        if (healthBar.hp <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void AddXP(int xp)
    {
        gainedXp = xp * hero.xpDropMultiplier;
    }
    private void CheckIfLevelUp()
    {
        if (gainedXp > 0)
        {
            float diff = xpBar.xpToNextLevel - xpBar.xp;
            if (diff > gainedXp)
            {
                xpBar.xp += gainedXp;
                gainedXp = 0;
            }
            else
            {
                xpBar.xp = 0;
                xpBar.xpToNextLevel *= 1.2f;
                gainedXp -= diff;

                upgradeUI.ShowUpgradeList(Upgrade.GetRandomUpgrades(hero));
                hero.lvl++;
            }
        }
    }
}

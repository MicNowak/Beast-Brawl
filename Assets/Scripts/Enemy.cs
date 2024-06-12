using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] EnemyHealthBar healthBar;
    public int xpDrop;
    public float hp, moveSpeed, fireRate, touchDamage, range;

    [Header("Bullet stats")]
    public float life;
    public float speed, size, damage;

    [Header("XP")]
    [SerializeField] GameObject xpPointPrefab;

    [Header("Spawners")]
    [SerializeField] GameObject spawner;

    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    GameObject target;
    float knockbackTime = 0.2f, knockbackTimmer = 0;
    Vector2 knockback = Vector2.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(hp);
            healthBar.SetHealth(hp);
        }
        if (spawner == null)
        {
            return;
        }
        BulletSpawner bulletSpawner = spawner.GetComponent<BulletSpawner>();
        bulletSpawner.bulletStats = new BulletStats()
        {
            players = false,
            speed = speed,
            size = size,
            damage = damage,
            life = life,
            penetration = 1,
        };
        bulletSpawner.fireRate = fireRate;
    }
    void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (spawner != null && Vector2.Distance(target.transform.position, transform.position) < range)
        {
            spawner.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            BulletSpawner bulletSpawner = spawner.GetComponent<BulletSpawner>();
            bulletSpawner.shoot = true;
            return;
        }
        else if (spawner != null)
        {
            BulletSpawner bulletSpawner = spawner.GetComponent<BulletSpawner>();
            bulletSpawner.shoot = false;
        }

        if (knockbackTimmer > 0)
        {
            rb.MovePosition(rb.position + knockback * Time.fixedDeltaTime);
            knockbackTimmer -= Time.fixedDeltaTime;
        }
        else
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            knockback = Vector2.zero;
        }
    }
    public void Hit(float damage, Vector2 knockback)
    {

        hp -= damage;
        this.knockback = knockback;
        knockbackTimmer = knockbackTime;
        if (hp <= 0)
        {
            GameObject xpPoint = Instantiate(xpPointPrefab, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            xpPoint.GetComponent<XpPoint>().xpValue = xpDrop;
            Destroy(gameObject);
        }
        if (healthBar != null)
        {
            healthBar.SetHealth(hp);
        }
    }
}

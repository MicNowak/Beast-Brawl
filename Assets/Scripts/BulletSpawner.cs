using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public BulletStats bulletStats;

    [Header("Spawner Settings")]
    public float fireRate = 1f;
    public bool shoot = false;

    float lastShot = 0f;

    void Update()
    {
        lastShot -= Time.deltaTime;
        if (!shoot || bulletPrefab == null || bulletStats == null)
        {
            return;
        }
        if (lastShot <= 0)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);

            bulletObj.GetComponent<Bullet>().stats = bulletStats;

            Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletStats.speed;
            rb.angularVelocity = bulletStats.rotationSpeed;

            bulletObj.transform.localScale = new Vector2(bulletStats.size, bulletStats.size);

            Destroy(bulletObj, bulletStats.life);
            lastShot = fireRate;
        }
    }
}

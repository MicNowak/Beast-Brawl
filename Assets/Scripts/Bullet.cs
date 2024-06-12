using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats stats;

    int hits = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stats == null)
        {
            return;
        }
        if (collision.CompareTag("Enemy") && stats.players)
        {
            Vector2 knockback = (collision.transform.position - transform.position).normalized * stats.knockback;
            Vector2 enemyPos = collision.transform.position;
            Debug.DrawLine(enemyPos, enemyPos + knockback, Color.red, 3f);
            collision.GetComponent<Enemy>().Hit(stats.damage, knockback);
            hits++;
        }
        else if (collision.CompareTag("Player") && !stats.players)
        {
            collision.GetComponent<Player>().Hit(stats.damage);
            hits++;
        }
        if (hits >= stats.penetration)
        {
            Destroy(gameObject);
        }
    }
}

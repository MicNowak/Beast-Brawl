using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] HealthBar healthBarLeft;
    [SerializeField] HealthBar healthBarRight;

    public void SetHealth(float health)
    {
        healthBarLeft.hp = health;
        healthBarRight.hp = health;
    }
    public void SetMaxHealth(float maxHealth)
    {
        healthBarLeft.maxHealth = maxHealth;
        healthBarRight.maxHealth = maxHealth;
    }

}

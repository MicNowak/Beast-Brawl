using UnityEngine;

public class EnemyMobSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] float timeBetweenSpawns;
    public GameObject[] enemies;
    float timmer;

    void Start()
    {
        timmer = timeBetweenSpawns;
    }
    void Update()
    {
        timmer -= Time.deltaTime;
        if (timmer <= 0)
        {
            timmer = timeBetweenSpawns;
            for (int i = 0; i < enemies.Length; i++)
            {
                enemySpawner.SpawnEnemy(enemies[i]);
            }
        }
    }
}

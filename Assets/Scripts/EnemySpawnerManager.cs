using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] int spawnerCount;
    [SerializeField] GameObject spawaner;
    int radius;
    void Start()
    {
        radius = (int)Camera.main.orthographicSize * 3;
        float step = 360 / spawnerCount;
        for (float angle = 0; angle < 360; angle += step)
        {
            Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius, 0);
            Instantiate(spawaner, pos, Quaternion.identity, transform);
        }
    }
    public void SpawnEnemy(GameObject enemyPrefab)
    {

        int spawnerIndex = Random.Range(0, spawnerCount);
        gameObject.transform.GetChild(spawnerIndex).GetComponent<EnemySpawner>().SpawnEnemy(enemyPrefab);
    }
    public void SpawnEnemies(List<GameObject> enemyPrefabList)
    {
        for (int i = 0; i < enemyPrefabList.Count; i++)
        {
            SpawnEnemy(enemyPrefabList[i]);
        }

    }
}

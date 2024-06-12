using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] TextMeshProUGUI timePassedText;
    [SerializeField] List<GameObject> easyEnemies;
    [SerializeField] List<GameObject> mediumEnemies;
    [SerializeField] List<GameObject> hardEnemies;
    [SerializeField] List<GameObject> bossEnemies;
    [SerializeField] EnemySpawnerManager enemySpawnerManager;
    float timePassed = 0f;

    public float spawnInterval = 1.7f; // Time between regular spawns
    public float difficultyIncreaseInterval = 30f; // Time to increase difficulty
    public float bossSpawnInterval = 180f; // Time between boss spawns

    private float timeSinceDifficultyIncrease = 0;
    private int bossIndex = 0;

    // Spawn probability weights
    private float easyWeight = 1f;
    private float mediumWeight = 0f;
    private float hardWeight = 0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnBossEnemy());
    }
    void Update()
    {
        timePassed += Time.deltaTime;
        spawnInterval -= Time.deltaTime * 0.001f;
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        timePassedText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && bossIndex >= bossEnemies.Count)
        {
            EndGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.HidePauseMenu();
            }
            else
            {
                pauseMenu.ShowPauseMenu();
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (bossIndex < bossEnemies.Count)
        {
            yield return new WaitForSeconds(spawnInterval);

            timeSinceDifficultyIncrease += spawnInterval;

            if (timeSinceDifficultyIncrease >= difficultyIncreaseInterval)
            {
                IncreaseDifficulty();
                timeSinceDifficultyIncrease = 0f;
            }

            SpawnEnemy();
        }
    }

    IEnumerator SpawnBossEnemy()
    {
        while (bossIndex < bossEnemies.Count)
        {
            yield return new WaitForSeconds(bossSpawnInterval);

            if (bossIndex < bossEnemies.Count && bossEnemies[bossIndex] != null)
            {
                enemySpawnerManager.SpawnEnemy(bossEnemies[bossIndex]);
                bossIndex++;
            }
        }
    }

    void IncreaseDifficulty()
    {
        if (easyWeight > 0)
        {
            easyWeight -= 0.1f;
            mediumWeight += 0.05f;
            hardWeight += 0.05f;
        }
        else if (mediumWeight > 0)
        {
            mediumWeight -= 0.1f;
            hardWeight += 0.1f;
        }
    }
    void SpawnEnemy()
    {
        GameObject enemyToSpawn = null;

        float totalWeight = easyWeight + mediumWeight + hardWeight;
        float randomValue = Random.Range(0f, totalWeight);

        if (randomValue < easyWeight)
        {
            enemyToSpawn = easyEnemies[Random.Range(0, easyEnemies.Count)];
        }
        else if (randomValue < easyWeight + mediumWeight)
        {
            enemyToSpawn = mediumEnemies[Random.Range(0, mediumEnemies.Count)];
        }
        else
        {
            enemyToSpawn = hardEnemies[Random.Range(0, hardEnemies.Count)];
        }

        if (enemyToSpawn != null)
        {
            enemySpawnerManager.SpawnEnemy(enemyToSpawn);
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(2);
    }

}

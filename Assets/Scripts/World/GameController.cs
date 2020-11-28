using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Spawner playerSpawner;

    public float EnemiesSpawnedPerSpawner;
    public int maxHealth;

    public GameObject Loading;
    public GameObject PlayerInput;
    public GameObject FinishedGO;

    public Button MoveUp;
    public Button MoveDown;
    public Button MoveLeft;
    public Button MoveRight;

    public Button ShootUp;
    public Button ShootDown;
    public Button ShootLeft;
    public Button ShootRight;

    private List<Spawner> enemySpawners;
    private List<GameObject> aliveEnemies;

    private int NumberOfEnemies;
    private int MaxNumberOfEnemies;
    private bool finished = false;

    private Finished finishedScript;

    void Awake()
    {
        enemySpawners = new List<Spawner>();
        aliveEnemies = new List<GameObject>();
        finishedScript = FinishedGO.GetComponent<Finished>();
        FinishedGO.SetActive(false);
    }

    public void StartGame()
    {
        playerSpawner.CustomStart();

        MaxNumberOfEnemies  = (int)((float)enemySpawners.Count * EnemiesSpawnedPerSpawner);

        NumberOfEnemies = MaxNumberOfEnemies;

        foreach (var item in enemySpawners)
        {
            item.CustomStart();
        }
        Loading.SetActive(false);
        PlayerInput.SetActive(true);
    }

    private void DestroyAll()
    {
        foreach (var sp in enemySpawners)
        {
            Destroy(sp.gameObject);
        }
        foreach (var sp in aliveEnemies)
        {
            Destroy(sp.gameObject);
        }
    }

    public void EnemyReachedObjective(GameObject gameObject)
    {
        Destroy(gameObject);
        maxHealth -= 1;
        if (maxHealth == 0 && !finished)
        {
            finished = true;
            FinishedGame("GAME OVER!");
        }
    }


    public bool IsLeftEnemies()
    {
        if (NumberOfEnemies == 0)
        {
            return false;
        }
        NumberOfEnemies -= 1;
        return true;
    }
    
    public void SubscribePlayerSpawner(Spawner spawner)
    {
        playerSpawner = spawner;
    }

    public void SubscribeEnemySpawner(Spawner spawner)
    {
        enemySpawners.Add(spawner);
    }

    public void UnSubscribeEnemySpawner(Spawner spawner)
    {
        enemySpawners.Remove(spawner);
    }

    public void SubscirbeEnemy(GameObject enemy)
    {
        aliveEnemies.Add(enemy);
    }

    public void UnsubscribeEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
        MaxNumberOfEnemies -= 1;
        if (MaxNumberOfEnemies == 0 && !finished)
        {
            FinishedGame("VICTORY!");
        }
    }

    private void FinishedGame(string text)
    {
        DestroyAll();
        finished = true;
        FinishedGO.SetActive(true);
        finishedScript.SetText(text);
    }

}

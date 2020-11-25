using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Spawner playerSpawner;
    List<Spawner> enemySpawners;

    void Start()
    {
        enemySpawners = new List<Spawner>();
    }

    public void StartGame()
    {
        playerSpawner.CustomStart();
    }
    
    public void SubscribePlayerSpawner(Spawner spawner)
    {
        playerSpawner = spawner;
    }

    public void SubscribeEnemySpawner(Spawner spawner)
    {
        enemySpawners.Add(spawner);
    }
}

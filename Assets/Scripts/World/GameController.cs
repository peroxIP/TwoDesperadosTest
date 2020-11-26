using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Spawner playerSpawner;

    public Button MoveUp;
    public Button MoveDown;
    public Button MoveLeft;
    public Button MoveRight;

    List<Spawner> enemySpawners;

    void Awake()
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

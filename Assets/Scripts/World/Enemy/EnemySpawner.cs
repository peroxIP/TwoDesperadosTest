using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public float spawnRate;
    public float maxInitalDelay;

    public override void CustomStart()
    {
        float val = Random.Range(1, maxInitalDelay);
        StartCoroutine(SpawnEnemy(val));
    }

    public override void SubscribeToController()
    {        
        gameController.SubscribeEnemySpawner(this);
    }

    IEnumerator SpawnEnemy(float time)
    {
        
        yield return new WaitForSeconds(time);
        if(gameController.IsLeftEnemies())
        {
            Spawn();
            StartCoroutine(SpawnEnemy(spawnRate));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameController.UnSubscribeEnemySpawner(this);
    }
}

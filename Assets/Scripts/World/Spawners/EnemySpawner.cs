using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public float spawnRate;
    public float maxInitalDelay;

    public override void CustomStart()
    {
        throw new System.NotImplementedException();
    }

    public override void SubscribeToController()
    {
        gameController.SubscribeEnemySpawner(this);
    }
}

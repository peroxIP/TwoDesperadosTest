using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    public override void CustomStart()
    {
        Spawn();
    }

    public override void SubscribeToController()
    {
        gameController.SubscribePlayerSpawner(this);
    }    
}

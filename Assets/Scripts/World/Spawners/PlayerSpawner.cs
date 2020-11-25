using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    
    public override void CustomStart()
    {
        SpawnPlayer(world.GetStarting());
    }

    public override void SubscribeToController()
    {
        gameController.SubscribePlayerSpawner(this);
    }

    private void SpawnPlayer(GameObject location)
    {
        Debug.Log(location.transform.position);
        GameObject spawn = Instantiate(toSpawn, new Vector3(location.transform.position.x, location.transform.position.y, -2), Quaternion.identity);
    }
}

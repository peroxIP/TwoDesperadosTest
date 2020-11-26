using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    public override void CustomStart()
    {
        SpawnPlayer(world.GetStartingTilePosition());
    }

    public override void SubscribeToController()
    {
        gameController.SubscribePlayerSpawner(this);
    }

    private void SpawnPlayer(Vector2Int location)
    {
        GameObject spawn = Instantiate(toSpawn, new Vector3(location.x, location.y, -2), Quaternion.identity);

        world.AdditionalSetup(spawn);
    }
}

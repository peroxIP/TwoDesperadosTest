using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Spawner : MonoBehaviour, IPartOfWorld, IGameControlled
{
    public GameObject toSpawn;

    protected World world;
    protected Vector2Int Position;
    protected GameController gameController;

    public void SetWorld(World world)
    {
        this.world = world;
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public void SetGameController(GameController controller)
    {
        this.gameController = controller;
        SubscribeToController();
    }

    public void Spawn()
    {
        GameObject spawn = Instantiate(toSpawn, new Vector3(Position.x, Position.y, -2), Quaternion.identity);

        world.AdditionalSetup(spawn);

        world.AddActorToPosition(spawn, Position);
    }

    public abstract void SubscribeToController();

    public abstract void CustomStart();
}

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

    public GameObject Spawn(bool Start = true)
    {
        GameObject spawn = Instantiate(toSpawn, new Vector3(Position.x, Position.y, -2), Quaternion.identity);
        var components = spawn.GetComponents<IGameControlled>();        
        
        world.AdditionalSetup(spawn);

        if (Start)
        {
            foreach (var item in components)
            {
                item.CustomStart();
            }
        }

        return spawn;
    }

    public abstract void SubscribeToController();

    public abstract void CustomStart();
}

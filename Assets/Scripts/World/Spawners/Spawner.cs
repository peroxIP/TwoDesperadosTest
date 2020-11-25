using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Spawner : MonoBehaviour
{
    public GameObject toSpawn;

    protected World world;
    protected GameController gameController;

    public void SetWorld(World world)
    {
        this.world = world;
    }

    public void SetGameController(GameController controller)
    {
        this.gameController = controller;
        SubscribeToController();
    }

    public abstract void SubscribeToController();
    public abstract void CustomStart();
}

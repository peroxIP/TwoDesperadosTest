using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour, IPartOfWorld, IGameControlled
{

    World World;
    GameController gameController;

    public void CustomStart()
    {
        
    }

    public void SetGameController(GameController controller)
    {
        gameController = controller;
    }

    public void SetWorld(World world)
    {
        World = world;
    }

    public void SubscribeToController()
    {
        
    }
}

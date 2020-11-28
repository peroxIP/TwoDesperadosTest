using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Movement : MonoBehaviour, IPartOfWorld, IGameControlled
{
    public float movementDelay;
    public RudimentalCollision myCollision;

    protected float currentMovementDelay = 0;
    protected Vector2Int Position;

    protected World World;
    protected GameController gameController;

    protected bool IsMovementDelayActive()
    {
        if (currentMovementDelay > 0)
        {
            currentMovementDelay -= Time.deltaTime;
            return true;
        }
        return false;
    }

    protected bool MoveTo(Vector2Int dir)
    {
        bool ok = World.IsMovementPosible(Position, dir);
        if (ok)
        {
            World.RemoveActorFromPosition(Position, myCollision);
            Position += dir;
            World.AddActorToPosition(Position, myCollision);

            currentMovementDelay = movementDelay;
            transform.position = new Vector3(Position.x, Position.y, transform.position.z);
        }
        return ok;
    }

    public void SetWorld(World w)
    {
        World = w;

        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public abstract void SubscribeToController();

    public abstract void CustomStart();

    public void SetGameController(GameController controller)
    {
        gameController = controller;
        SubscribeToController();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Movement : MonoBehaviour, IPartOfWorld, IGameControlled
{
    public float movementDelay;

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

    protected void MoveTo(Vector2Int dir)
    {
        bool ok = World.IsMovementPosible(Position, dir);
        if (ok)
        {
            World.RemoveActorFromPosition(this.gameObject, Position);
            Position = Position + dir;
            World.AddActorToPosition(this.gameObject, Position);

            currentMovementDelay = movementDelay;
            transform.position = new Vector3(Position.x, Position.y, transform.position.z);
        }
    }
    
    public abstract void SetWorld(World w);

    public abstract void SubscribeToController();

    public abstract void CustomStart();

    public abstract void SetGameController(GameController controller);
}

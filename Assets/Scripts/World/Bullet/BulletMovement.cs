using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : Movement
{
    Vector2Int Direction;


    public override void CustomStart()
    {
        StartCoroutine(MoveBullet(movementDelay));
    }

    public void SetCollisionTag(RudimentalCollision parent)
    {
        myCollision = parent;
        myCollision.Movement = this;
    }


    IEnumerator MoveBullet(float time)
    {
        yield return new WaitForSeconds(time);

        bool ok = MoveTo(Direction);
        if (ok)
        {
            StartCoroutine(MoveBullet(movementDelay));
        }
        else
        {
            World.RemoveActorFromPosition(Position, myCollision);
            Destroy(this.gameObject);
        }
    }

    public override void SubscribeToController()
    {
    }

    internal void SetDirection(Vector2Int dir)
    {
        Direction = dir;
    }
}

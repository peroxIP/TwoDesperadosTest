using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : Movement
{
    Vector2Int Direction;
    public override void CustomStart()
    {
        Tag = CollisionTag.Bullet;
        StartCoroutine(MoveBullet(movementDelay));
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

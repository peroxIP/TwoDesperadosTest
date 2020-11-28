using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    Stack<Vector2Int> Path;

    public override void CustomStart()
    {
        myCollision = new RudimentalCollision(CollisionTag.Enemy, this, new List<CollisionTag> { CollisionTag.PlayerBullet, CollisionTag.Player});

        Path = World.FindPathToOjbective(Position);

        if (Path.Count != 0)
        {
            StartCoroutine(MoveEnemy(movementDelay));
        }
    }
    
    IEnumerator MoveEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        
        Vector2Int nextTilePos = Path.Pop();

        Vector2Int dir = (nextTilePos -  Position);
        
        MoveTo(dir);
        if (Path.Count != 0)
        {
            StartCoroutine(MoveEnemy(movementDelay));
        }
        else
        {
            gameController.EnemyReachedObjective(this.gameObject);
        }
    }

    public override void SubscribeToController()
    {
        gameController.SubscirbeEnemy(this.gameObject);
    }

    private void OnDestroy()
    {
        gameController.UnsubscribeEnemy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    Stack<Vector2Int> Path;

    public override void CustomStart()
    {
        throw new System.NotImplementedException();
    }

    public override void SetGameController(GameController controller)
    {
        gameController = controller;
        SubscribeToController();
    }

    private void Start()
    {
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

    public override void SetWorld(World w)
    {
        World = w;
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Path = World.FindPathToOjbective(Position);
        World.AddActorToPosition(this.gameObject, Position);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    int horizontal = 0;
    int vertical = 0;

    void Update()
    {        
        if (IsMovementDelayActive())
        {
            return;
        }
#if UNITY_STANDALONE
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal == 0 && vertical == 0)
        {
            return;
        }

        if (horizontal != 0)
        {
            vertical = 0;
        }
#endif
        Vector2Int dir = new Vector2Int(horizontal, vertical);
        MoveTo(dir);
    }

    public void GoLeft()
    {
        MoveTo(Vector2Int.left);
    }

    public void GoRight()
    {
        MoveTo(Vector2Int.right);
    }

    public void GoUp()
    {
        MoveTo(Vector2Int.up);
    }

    public void GoDown()
    {
        MoveTo(Vector2Int.down);
    }

    public override void SubscribeToController()
    {
        //debilana delux, can't assign game objects to prefab
        gameController.MoveUp.onClick.AddListener(GoUp);
        gameController.MoveDown.onClick.AddListener(GoDown);
        gameController.MoveLeft.onClick.AddListener(GoLeft);
        gameController.MoveRight.onClick.AddListener(GoRight);

    }

    public override void CustomStart()
    {
        myCollision = new RudimentalCollision(CollisionTag.Player, this, new List<CollisionTag> { CollisionTag.Enemy , CollisionTag.EnemyBullet });
    }

    private void OnDestroy()
    {
        gameController.PlayerDestroyed();
    }
}

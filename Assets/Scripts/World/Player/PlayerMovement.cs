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


    public override void SetGameController(GameController controller)
    {
        gameController = controller;
        //debilana delux, can't assign game objects to prefab
        controller.MoveUp.onClick.AddListener(GoUp);
        controller.MoveDown.onClick.AddListener(GoDown);
        controller.MoveLeft.onClick.AddListener(GoLeft);
        controller.MoveRight.onClick.AddListener(GoRight);
    }

    public override void SetWorld(World w)
    {
        World = w;
        Position = World.GetStartingTilePosition();
        World.AddActorToPosition(this.gameObject, Position);
    }

    public override void SubscribeToController()
    {
        throw new System.NotImplementedException();
    }

    public override void CustomStart()
    {
        throw new System.NotImplementedException();
    }
}

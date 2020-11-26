using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour, IPartOfWorld, IGameControlled
{
    

    public float movementDelay;

    private float currentMovementDelay = 0;
    private Vector2Int position;

    World world;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;      
        int vertical = 0;

        if (currentMovementDelay > 0)
        {
            currentMovementDelay -= Time.deltaTime;
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
        
        
        //Debug.Log("HORIZONTAL: " + horizontal + " VERTICAL: " + vertical);

    }

    public void SetWorld(World w)
    {
        world = w;
        position = world.GetStartingTilePosition();
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

    private void MoveTo(Vector2Int dir)
    {
        bool ok = world.IsMovementPosible(position, dir);
        if (ok)
        {
            position = position + dir;
            currentMovementDelay = movementDelay;
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        else
        {
            Debug.Log("NE");
        }
    }

    public void SetGameController(GameController controller)
    {
        //debilana delux, cant assign game objects to prefab
        controller.MoveUp.onClick.AddListener(GoUp);
        controller.MoveDown.onClick.AddListener(GoDown);
        controller.MoveLeft.onClick.AddListener(GoLeft);
        controller.MoveRight.onClick.AddListener(GoRight);
    }

    public void SubscribeToController()
    {
        throw new NotImplementedException();
    }

    public void CustomStart()
    {
        throw new NotImplementedException();
    }
}

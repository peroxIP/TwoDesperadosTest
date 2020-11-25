using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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
        bool ok = world.isMovementPosible(position, dir);
        if(ok)
        {
            position = position + dir;
            currentMovementDelay = movementDelay;
            transform.position = new Vector3(transform.position.x + horizontal, transform.position.y + vertical, transform.position.z);
        }
        else
        {
            Debug.Log("NE");
        }
        
        //Debug.Log("HORIZONTAL: " + horizontal + " VERTICAL: " + vertical);

    }

    internal void SetWorld(World w)
    {
        world = w;
        position = world.GetStartingTilePosition();
    }
}

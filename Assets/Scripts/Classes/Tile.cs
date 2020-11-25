using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private bool obsticle;
    // N, E, S, W
    public bool[] walls { get; }

    public Vector2Int position { get;}

    public bool isVisited = false;
    public bool isOccupied = false;

    public Direction neighborAs;
    public Tile(int x, int y)
    {
        this.position = new Vector2Int(x,y);

        obsticle = false;

        walls = new bool[4];

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = true;
        }
    }

    public string GetName()
    {
        return "Floor " + position;
    }

    override
    public string ToString()
    {
        return GetName() + " isVisited: " + isVisited + " neighborAs:" + neighborAs;
    }
}

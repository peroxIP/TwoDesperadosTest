using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private bool obsticle;
    // N, E, S, W
    public bool[] walls { get; }

    public int x { get;}
    public int y { get;}

    public bool isVisited = false;

    public Direction neighborAs;
    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;

        obsticle = false;

        walls = new bool[4];

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = true;
        }
    }

    public string GetName()
    {
        return "Floor x=" + x + " y=" + y;
    }

    override
    public string ToString()
    {
        return "Floor x=" + x + " y=" + y + " isVisited: " + isVisited + " neighborAs:" + neighborAs;
    }
}

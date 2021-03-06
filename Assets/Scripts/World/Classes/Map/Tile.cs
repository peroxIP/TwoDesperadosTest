﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private bool obsticle;
    // N, E, S, W
    public bool[] walls;

    public Vector2Int position { get;}

    public bool isVisited = false;
    public bool isOccupied = false;

    public float f, g, h = 0;
    public Tile Parent;

    public Direction neighborAs;

    private List<GameObject> Actors;

    public Tile(int x, int y)
    {
        this.position = new Vector2Int(x,y);

        Actors = new List<GameObject>();

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
    
    public override string ToString()
    {
        return GetName() + " isVisited: " + isVisited + " neighborAs:" + neighborAs;
    }
}

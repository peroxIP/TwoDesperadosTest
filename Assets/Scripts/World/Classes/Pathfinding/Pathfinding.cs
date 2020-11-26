using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    protected Grid grid;

    protected Stack<Tile> path;

    public Pathfinding(Grid grid)
    {
        this.grid = grid;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinding
{
    protected Grid grid;

    protected Stack<Tile> path;
    protected Queue<Tile> open;

    public Pathfinding(Grid grid)
    {
        path = new Stack<Tile>();
        open = new Queue<Tile>();
        this.grid = grid;
    }

    public abstract Stack<Vector2Int> FindPathTo(Vector2Int src, Vector2Int dest);
}

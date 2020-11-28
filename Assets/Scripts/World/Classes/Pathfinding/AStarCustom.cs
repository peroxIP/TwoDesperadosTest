using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarCustom : Pathfinding
{
    private readonly int MaxV = 1000;

    public AStarCustom(Grid grid) : base(grid)
    {
    }

    private void ResetAllVisited()
    {
        open = new Queue<Tile>();
        foreach (var tile in grid.GetTiles())
        {
            tile.isVisited = false;
            tile.f = MaxV;
            tile.h = MaxV;
            tile.g = MaxV;
            tile.Parent = null;
        }
    }

    private float CalculateHValue(Vector2Int current, Vector2Int dest)
    {
        return Mathf.Sqrt( (current.x - dest.x) * (current.x - dest.x)
                        + (current.y - dest.y) * (current.y - dest.y));
    }

    public override Stack<Vector2Int> FindPathTo(Vector2Int src, Vector2Int dest)
    {
        ResetAllVisited();

        Tile start = grid.GetTile(src);
        start.f = 0;
        start.h = 0;
        start.g = 0;
        start.Parent = null;
        Tile finish = grid.GetTile(dest);
        Tile current;
         bool found = false;

        float gNew;
        float hNew;
        float fNew;

        open.Enqueue(start);

        while (!found && open.Count != 0)
        {
            current = open.Dequeue();
            List<Tile> list = grid.GetTileNeighboursThatCanBeWisited(current);

            foreach (var t in list)
            {
                t.isVisited = true;

                if (t == finish)
                {
                    found = true;
                    t.Parent = current;
                    break;
                }

                gNew = current.g + 1;
                hNew = CalculateHValue(current.position, dest);
                fNew = gNew + hNew;

                if (t.f == MaxV || t.f > fNew)
                {
                    open.Enqueue(t);

                    t.f = fNew;
                    t.g = gNew;
                    t.h = hNew;
                    t.Parent = current;
                }

            }
            if (found)
            {
                break;
            }
        }
        if (open.Count == 0 || !found)
        {
            Debug.Log("No path found");
        }

        Stack<Vector2Int> l = Backtrack(start, finish);
        return l;
    }

    private Stack<Vector2Int> Backtrack(Tile src, Tile dest)
    {
        Stack<Vector2Int> temp = new Stack<Vector2Int>();
        temp.Push(dest.position);

        Tile current = dest.Parent;
        bool done = false;
        while (!done)
        {
            if (current == src)
            {
                break;
            }
            temp.Push(current.position);
            current = current.Parent;
        }
        return temp;
    }
}

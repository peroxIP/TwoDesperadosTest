using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAndKill : Pathfinding
{
    public HuntAndKill(Grid grid) : base(grid)
    {
    }

    public void HuntWalls()
    {
        int i = 0;
        int j = 0;
        while (i != grid.width && j != grid.height)
        {
            for (i = 0; i < grid.width; i++)
            {
                for (j = 0; j < grid.height; j++)
                {
                    KillWalls(grid.GetTile(i,j));
                }
            }
        }
    }

    public void KillWalls(Tile tile)
    {
        List<Tile> neighbours = grid.GetTileNeighbours(tile);

        if (neighbours.Count == 0)
        {
            return;
        }
        int randomTileIndex = Random.Range(0, neighbours.Count);

        Tile neighbour = neighbours[randomTileIndex];

        KillAllSuroundingWalls(tile, neighbour);

        KillWalls(neighbour);
    }

    public void KillAllSuroundingWalls(Tile mainTile, Tile neighbour)
    {
        mainTile.isVisited = true;
        neighbour.isVisited = true;
        switch (neighbour.neighborAs)
        {
            case Direction.NORTH:
                mainTile.walls[(int)Direction.NORTH] = false;
                neighbour.walls[(int)Direction.SOUTH] = false;
                break;
            case Direction.EAST:
                mainTile.walls[(int)Direction.EAST] = false;
                neighbour.walls[(int)Direction.WEST] = false;
                break;
            case Direction.SOUTH:
                mainTile.walls[(int)Direction.SOUTH] = false;
                neighbour.walls[(int)Direction.NORTH] = false;
                break;
            case Direction.WEST:
                mainTile.walls[(int)Direction.WEST] = false;
                neighbour.walls[(int)Direction.EAST] = false;
                break;
            default:
                break;
        }
    }
}

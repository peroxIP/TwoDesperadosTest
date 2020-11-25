using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public Tile playerStartingPoint { get; }

    private int width;
    private int height;
    private Tile[,] tiles;

    private delegate bool Checker(Tile tile);

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        GenerateTiles();

        int fwidth = width / 4;
        int fheight = height / 4;

        int randX = Random.Range(fwidth, width - fwidth);
        int randY = Random.Range(fheight, height - fheight);

        playerStartingPoint = tiles[randX, randY];

        List<Tile> neighbours = GetTileNeighbours(playerStartingPoint, NoCheck);

        KillWalls(playerStartingPoint);

        HuntWalls();
        WUT();

        PrepareStartingPoint(playerStartingPoint, neighbours);
    }

    private void WUT()
    {
        Tile last = tiles[0, 0];
        for (int i = 0; i < width ; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile current = tiles[i, j];
                if (!current.isVisited)
                {
                    List<Tile> neighbours = GetTileNeighbours(current, NoCheck);
                    int randomTileIndex = Random.Range(0, neighbours.Count);
                    Tile neighbour = neighbours[randomTileIndex];
                    KillAllSuroundingWalls(current, neighbour);
                }
            }
        }
    }

    private void HuntWalls()
    {        
        for (int i = 0; i < width ; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile current = tiles[i, j];
                if (!current.isVisited)
                {
                    List<Tile> neighbours = GetTileNeighbours(current, NoCheck);
                    int randomTileIndex = Random.Range(0, neighbours.Count);
                    Tile neighbour = neighbours[randomTileIndex];
                    KillWalls(neighbour);
                }
            }
        }
    }

    private void PrepareStartingPoint(Tile tile, List<Tile> neighbours)
    {
        foreach (var neighbour in neighbours)
        {
            KillAllSuroundingWalls(tile, neighbour);
        }
    }

    

    private bool NoCheck(Tile tile)
    {
        return false;
    }

    private bool CheckIsVisible(Tile tile)
    {
        return tile.isVisited;
    }

    private List<Tile> GetTileNeighbours(Tile tile, Checker fun)
    {
        int i = tile.x;
        int j = tile.y;
        List<Tile> neigbours = new List<Tile>();

        int north = j + 1;
        int south = j - 1;
        int east = i + 1;
        int west = i - 1;

        Tile t;

        if (!IsOutOfBounds(north, height))
        {
            t = tiles[i, north];
            t.neighborAs = Direction.NORTH;
            if (!fun(t))
            {
                neigbours.Add(t);
            }
        }

        if (!IsOutOfBounds(south, height))
        {
            t = tiles[i, south];
            t.neighborAs = Direction.SOUTH;
            if (!fun(t))
            {
                neigbours.Add(t);
            }
        }

        if (!IsOutOfBounds(east, width))
        {
            t = tiles[east, j];
            t.neighborAs = Direction.EAST;
            if (!fun(t))
            {
                neigbours.Add(t);
            }
        }
        if (!IsOutOfBounds(west, width))
        {
            t = tiles[west, j];
            t.neighborAs = Direction.WEST;
            if (!fun(t))
            {
                neigbours.Add(t);
            }
        }

        return neigbours;
    }

    public ref Tile[,] GetTiles()
    {
        return ref tiles;
    }

    private bool IsOutOfBounds(int index, int max)
    {
        if (index < 0 || index > max - 1)
        {
            return true;
        }
        return false;
    }

    private void GenerateTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i,j] = new Tile(i , j);
            }
        }
    }
    
    private void KillWalls(Tile tile)
    {
        List<Tile> neighbours = GetTileNeighbours(tile, CheckIsVisible);

        if (neighbours.Count == 0 )
        {
            return;
        }
        int randomTileIndex = Random.Range(0, neighbours.Count);

        Tile neighbour = neighbours[randomTileIndex];

        KillAllSuroundingWalls(tile, neighbour);

        KillWalls(neighbour);
    }

    private void KillAllSuroundingWalls(Tile mainTile, Tile neighbour)
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

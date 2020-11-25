using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid
{
    private Tile playerStartingTile;

    private int numberOfSpawners;
    private List<Tile> spawnerTiles;

    private int width;
    private int height;

    private int fwidth;
    private int fheight;

    private Tile[,] tiles;


    private delegate Vector2Int DirectionHelper(Vector2Int v);

    private Dictionary<Direction, DirectionHelper> GetDirection;
    
    private readonly List<Direction> NESW = new List<Direction>
        {
            Direction.NORTH,
            Direction.EAST,
            Direction.SOUTH,
            Direction.WEST
        };

    private readonly Vector2Int ErrorVector = new Vector2Int(-1, -1);

    public Grid(int width, int height, int seed)
    {
        GetDirectionFactory();
        
        this.width = width;
        this.height = height;

        fwidth = width / 4;
        fheight = height / 4;

        spawnerTiles = new List<Tile>();

        numberOfSpawners = (width + height) / 4;

        if (seed !=0)
        {
            Random.InitState(seed);
        }

        tiles = new Tile[width, height];

        GenerateTiles();
        HuntWalls();
        // TODO: CLEANUP
        PreparePlayerStartingPoint();
        PrepareSpawnerStartingPoints();
    }

    public ref Tile[,] GetTiles()
    {
        return ref tiles;
    }

    public ref Tile GetStartingTile()
    {
        return ref playerStartingTile;
    }

    public ref List<Tile> GetSpawnerPositions()
    {
        return ref spawnerTiles;
    }

    private void GetDirectionFactory()
    {
        GetDirection = new Dictionary<Direction, DirectionHelper>
        {
            { Direction.NORTH, vector => vector + Vector2Int.up },
            { Direction.SOUTH, vector => vector + Vector2Int.down },
            { Direction.EAST, vector => vector + Vector2Int.right },
            { Direction.WEST, vector => vector + Vector2Int.left },

            { Direction.NE, vector => vector + Vector2Int.up + Vector2Int.right},
            { Direction.NW, vector => vector + Vector2Int.up + Vector2Int.left},

            { Direction.SE, vector => vector + Vector2Int.down + Vector2Int.right},
            { Direction.SW, vector => vector + Vector2Int.down + Vector2Int.left},
        };        
    }

    private void PrepareSpawnerStartingPoints()
    {
        for (int i = 0; i < numberOfSpawners; i++)
        {
            Tile potential = GetPotentialTileForSpawner();
            Debug.Log("SPAWNER TILE " + numberOfSpawners + ": " + potential.ToString());

            spawnerTiles.Add(potential);

            List<Tile> neighbours = GetTileNeighbours(potential, GetDirection.Keys.ToList());

            foreach (var neighbour in neighbours)
            {
                neighbour.isOccupied = true;
            }
        }
    }

    private Tile GetPotentialTileForSpawner()
    {
        int x = 0;
        int y = 0;
        int intDir = Random.Range(0, 4);

        Direction direction = (Direction)intDir;

        switch (direction)
        {
            case Direction.NORTH:
                x = Random.Range(1, width - 1);
                y = Random.Range(height - fheight, height - 1);
                break;
            case Direction.EAST:
                x = Random.Range(width - fwidth, width - 1);
                y = Random.Range(1, height - 1);
                break;
            case Direction.SOUTH:
                x = Random.Range(1, width - 1);
                y = Random.Range(1, fheight);
                break;
            case Direction.WEST:
                x = Random.Range(1, fwidth);
                y = Random.Range(1, height - 1);
                break;
            default:
                break;
        }

        Tile potentialTile = tiles[x, y];
        if (!potentialTile.isOccupied)
        {
            return potentialTile;
        }
        else
        {
            return GetPotentialTileForSpawner();
        }
    }

    private void PreparePlayerStartingPoint()
    {
        int randX = Random.Range(fwidth, width - fwidth);
        int randY = Random.Range(fheight, height - fheight);

        playerStartingTile = tiles[randX, randY];

        playerStartingTile.isOccupied = true;

        Debug.Log("STARTING " + playerStartingTile.ToString());
        List<Tile> neighbours = GetTileNeighbours(playerStartingTile, GetDirection.Keys.ToList());

        foreach (var neighbour in neighbours)
        {
            neighbour.isOccupied = true;

            KillAllSuroundingWalls(playerStartingTile, neighbour);
        }
    }

    private void HuntWalls()
    {
        int i = 0;
        int j = 0;
        while (i != width && j != height)
        {
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    KillWalls(tiles[i, j]);
                }
            }
        }
    }

    private List<Tile> GetTileNeighbours(Tile tile)
    {
        return GetTileNeighbours(tile, NESW, true);
    }

 

    private List<Tile> GetTileNeighbours(Tile tile, List<Direction> directions, bool CheckIsVisited = false)
    {
        List<Tile> neigbours = new List<Tile>();

        foreach (Direction direction in directions)
        {
            DirectionHelper func = GetDirection[direction];

            Vector2Int neighborPosition = func(tile.position);

            neighborPosition = IsOutOfBounds(neighborPosition);

            if (neighborPosition != ErrorVector)
            {
                Tile neighbor = GetTileAt(neighborPosition);
                if (CheckIsVisited && neighbor.isVisited)
                {
                    continue;
                }
                neigbours.Add(neighbor);
                neighbor.neighborAs = direction;
            }
        }
        return neigbours;
    }

    private Vector2Int IsOutOfBounds(Vector2Int position)
    {
        if (position.x < 0 ||
            position.x > width - 1 ||
            position.y < 0 ||
            position.y > height - 1)
        {
            return ErrorVector;
        }
        return position;
    }

    private Tile GetTileAt(Vector2Int position)
    {
        return tiles[position.x, position.y];
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
        List<Tile> neighbours = GetTileNeighbours(tile);

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

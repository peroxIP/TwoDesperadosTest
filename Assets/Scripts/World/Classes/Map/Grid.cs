using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid
{
    private Tile playerStartingTile;

    private int numberOfSpawners;
    private List<Tile> spawnerTiles;

    public readonly int width;
    public readonly int height;

    public readonly int fwidth;
    public readonly int fheight;

    private Tile[,] tiles;

    HuntAndKill huntAndKill;

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
        //numberOfSpawners = 1;

        if (seed !=0)
        {
            UnityEngine.Random.InitState(seed);
        }

        tiles = new Tile[width, height];

        GenerateTiles();

        huntAndKill = new HuntAndKill(this);

        huntAndKill.HuntWalls();

        PreparePlayerStartingPoint();
        PrepareSpawnerStartingPoints();
    }

    public IEnumerable<Tile> GetTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                yield return tiles[i, j];
            }
        }
    }

    public Tile GetTile(Vector2Int position)
    {
        return tiles[position.x, position.y];
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
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
            potential.isOccupied = true;

            spawnerTiles.Add(potential);

            List<Tile> neighbours = GetTileNeighbours(potential, GetDirection.Keys.ToList());

            foreach (var neighbour in neighbours)
            {
                neighbour.isOccupied = true;
            }
        }
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



    private Tile GetPotentialTileForSpawner()
    {
        int x = 0;
        int y = 0;
        int intDir = UnityEngine.Random.Range(0, 4);

        Direction direction = (Direction)intDir;

        switch (direction)
        {
            case Direction.NORTH:
                x = UnityEngine.Random.Range(1, width - 1);
                y = UnityEngine.Random.Range(height - fheight, height - 1);
                break;
            case Direction.EAST:
                x = UnityEngine.Random.Range(width - fwidth, width - 1);
                y = UnityEngine.Random.Range(1, height - 1);
                break;
            case Direction.SOUTH:
                x = UnityEngine.Random.Range(1, width - 1);
                y = UnityEngine.Random.Range(1, fheight);
                break;
            case Direction.WEST:
                x = UnityEngine.Random.Range(1, fwidth);
                y = UnityEngine.Random.Range(1, height - 1);
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
        int randX = UnityEngine.Random.Range(fwidth, width - fwidth);
        int randY = UnityEngine.Random.Range(fheight, height - fheight);

        playerStartingTile = tiles[randX, randY];

        playerStartingTile.isOccupied = true;

        List<Tile> neighbours = GetTileNeighbours(playerStartingTile, GetDirection.Keys.ToList());

        foreach (var neighbour in neighbours)
        {
            neighbour.isOccupied = true;

            huntAndKill.KillAllSuroundingWalls(playerStartingTile, neighbour);
        }
    }

    public List<Tile> GetTileNeighbours(Tile tile)
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
                Tile neighbor = GetTile(neighborPosition);
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

    public List<Tile> GetTileNeighboursThatCanBeWisited(Tile tile)
    {
        List<Tile> neigbours = new List<Tile>();
        foreach (Direction direction in NESW)
        {
            DirectionHelper func = GetDirection[direction];

            Vector2Int neighborPosition = func(tile.position);

            neighborPosition = IsOutOfBounds(neighborPosition);

            if (neighborPosition != ErrorVector)
            {                
                if (!tile.walls[(int) direction])
                {
                    Tile neighbor = GetTile(neighborPosition);
                    neigbours.Add(neighbor);
                }
            }
        }
        return neigbours;
    }


    public bool IsMovementPosible(Vector2Int currentPosition , Vector2Int dir)
    {

        Vector2Int neighborPosition = currentPosition + dir;

        neighborPosition = IsOutOfBounds(neighborPosition);

        

        if (neighborPosition != ErrorVector)
        {
            Tile current = GetTile(currentPosition);

            if (dir == Vector2Int.up)
            {
                return !current.walls[(int)Direction.NORTH];
            }
            else if (dir == Vector2Int.down)
            {
                return !current.walls[(int)Direction.SOUTH];
                
            }
            else if (dir == Vector2Int.left)
            {
                return !current.walls[(int)Direction.WEST];
            }
            else if (dir == Vector2Int.right)
            {
                return !current.walls[(int)Direction.EAST];
            }
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




}

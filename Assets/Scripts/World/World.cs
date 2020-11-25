using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int width;
    public int height;
    public int obsticleCount;
    public int seed;

    public GameObject gameControllerObject;

    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public GameObject startingPositionPrefab;
    public GameObject spawnerPrefab;

    private GameObject[,] allFloors;
    private GameObject[] spawners;
    private GameObject startingPosition;

    private Grid grid;

    private GameController gameController;

    private void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();

        PopulateValues();
        allFloors = new GameObject[width, height];
        spawners = new GameObject[(width + height) /4];
        grid = new Grid(width, height, seed);
        
        InstantiateFloorsAndWalls(ref grid.GetTiles());
        InstantiateStartingPosition(grid.GetStartingTile().position);
        InstantiateSpawners(grid.GetSpawnerPositions());

        gameController.StartGame();
    }

    public ref GameObject[] GetSpawners()
    {
        return ref spawners;
    }

    public ref GameObject GetStarting()
    {
        return ref startingPosition;
    }


    private void InstantiateSpawners(List<Tile> tiles)
    {
        int index = 0;
        foreach (Tile tile in tiles)
        {
            Vector2Int position = tile.position;
            GameObject spawner = Instantiate(spawnerPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
            spawner.name = "Spawner " + index;
            spawners[index++] = spawner;

            Spawner spawnerScript = spawner.GetComponent<Spawner>();
            spawnerScript.SetWorld(this);
            spawnerScript.SetGameController(gameController);
        }
    }

    private void InstantiateStartingPosition(Vector2Int position)
    {
        startingPosition = Instantiate(startingPositionPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
        startingPosition.name = "Starting";

        Spawner spawnerScript = startingPosition.GetComponent<Spawner>();
        spawnerScript.SetWorld(this);
        spawnerScript.SetGameController(gameController);
    }

    private void PopulateValues()
    {
        if (SettingValues.Width != 0)
        {
            width = SettingValues.Width;
        }
        if (SettingValues.Height != 0)
        {
            height = SettingValues.Height;
        }
        if (SettingValues.ObsticleCount != 0)
        {
            obsticleCount = SettingValues.ObsticleCount;
        }
    }

    private void InstantiateFloorsAndWalls(ref Tile[,] tiles)
    {
        float xi;
        float xj;
        Tile t;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                xi = 1f * i;
                xj = 1f * j;

                t = tiles[i, j];

                GameObject floor = Instantiate(tilePrefab, new Vector3(xi, xj, 0), Quaternion.identity);

                floor.name = t.GetName();
                floor.transform.parent = transform;
                allFloors[i, j] = floor;


                // walls 
                if (t.walls[(int) Direction.NORTH])
                {
                    GameObject nWall = Instantiate(wallPrefab, new Vector3(xi, xj + 0.5f, -1), Quaternion.Euler(0, 0, 90));
                    nWall.name = "N Wall";
                    nWall.transform.parent = floor.transform;
                }

                if (t.walls[(int)Direction.SOUTH])
                {
                    GameObject sWall = Instantiate(wallPrefab, new Vector3(xi, xj - 0.5f, -1), Quaternion.Euler(0, 0, 90));
                    sWall.transform.parent = floor.transform;
                    sWall.name = "S Wall";
                }

                if (t.walls[(int)Direction.WEST])
                {
                    GameObject wWall = Instantiate(wallPrefab, new Vector3(xi - 0.5f, xj, -1), Quaternion.identity);
                    wWall.transform.parent = floor.transform;
                    wWall.name = "W Wall";
                }

                if (t.walls[(int)Direction.EAST])
                {
                    GameObject eWall = Instantiate(wallPrefab, new Vector3(xi + 0.5f, xj, -1), Quaternion.identity);
                    eWall.transform.parent = floor.transform;
                    eWall.name = "E Wall";
                }
            }
        }        
    }


}

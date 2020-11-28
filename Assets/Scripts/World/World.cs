﻿using System;
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
    
    private Grid grid;
    private Pathfinding pathfinding;

    private GameController gameController;

    private List<GameObject> additionalSetup;
    private int NumberOfSpawners;

    private void Awake()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        additionalSetup = new List<GameObject>();
        PopulateValues();
        grid = new Grid(width, height, seed);
        Camera.main.transform.position = new Vector3(width / 2, height / 2, Camera.main.transform.position.z);
        pathfinding = new AStarCustom(grid);
    }

    public void AddActorToPosition(GameObject gameObject, Vector2Int position)
    {
        grid.AddActorToPosition(gameObject, position);
    }

    public void RemoveActorFromPosition(GameObject gameObject, Vector2Int position)
    {
        grid.RemoveActorFromPosition(gameObject, position);
    }

    private void Start()
    {
        InstantiateFloorsAndWalls();
        InstantiateStartingPosition(grid.GetStartingTile().position);

        InstantiateSpawners(grid.GetSpawnerPositions());

        AdditionalSetup(additionalSetup);

        gameController.StartGame();
    }

    public Vector2Int GetStartingTilePosition()
    {
        return grid.GetStartingTile().position;
    }

    public bool IsMovementPosible(Vector2Int currentPosition, Vector2Int dir)
    {
        return grid.IsMovementPosible(currentPosition, dir);
    }

    public Stack<Vector2Int> FindPathToOjbective(Vector2Int src)
    {
        return pathfinding.FindPathTo(src, grid.GetStartingTile().position);
    }

    public void AdditionalSetup(GameObject gameObject)
    {
        IPartOfWorld[] partOfWorld = gameObject.GetComponents<IPartOfWorld>();
        if (partOfWorld != null && partOfWorld.Length > 0)
        {
            foreach (var item in partOfWorld)
            {
                item.SetWorld(this);
            }
        }

        IGameControlled[] gameControlled = gameObject.GetComponents<IGameControlled>();
        if (gameControlled != null && gameControlled.Length > 0)
        {
            foreach (var item in gameControlled)
            {
                item.SetGameController(gameController);
            }            
        }
    }

    public void AdditionalSetup(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            AdditionalSetup(gameObject);
        }
    }

    private void InstantiateSpawners(List<Tile> tiles)
    {
        int index = 0;
        foreach (Tile tile in tiles)
        {

            Vector2Int position = tile.position;
            GameObject spawner = Instantiate(spawnerPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
            spawner.name = "Spawner " + index;

            additionalSetup.Add(spawner);
        }
    }

    private void InstantiateStartingPosition(Vector2Int position)
    {
        GameObject startingPosition = Instantiate(startingPositionPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
        startingPosition.name = "Starting";

        additionalSetup.Add(startingPosition);
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

    private void InstantiateFloorsAndWalls()
    {
        foreach (Tile tile in grid.GetTiles())
        {
            int x = tile.position.x;
            int y = tile.position.y;

            float xi = 1f * x;
            float xj = 1f * y;

            GameObject floor = Instantiate(tilePrefab, new Vector3(xi, xj, 0), Quaternion.identity);

            floor.name = tile.GetName();
            floor.transform.parent = transform;

            // walls 
            if (tile.walls[(int)Direction.NORTH])
            {
                GameObject nWall = Instantiate(wallPrefab, new Vector3(xi, xj + 0.5f, -1), Quaternion.Euler(0, 0, 90));
                nWall.name = "N Wall";
                nWall.transform.parent = floor.transform;
            }

            if (tile.walls[(int)Direction.SOUTH])
            {
                GameObject sWall = Instantiate(wallPrefab, new Vector3(xi, xj - 0.5f, -1), Quaternion.Euler(0, 0, 90));
                sWall.transform.parent = floor.transform;
                sWall.name = "S Wall";
            }

            if (tile.walls[(int)Direction.WEST])
            {
                GameObject wWall = Instantiate(wallPrefab, new Vector3(xi - 0.5f, xj, -1), Quaternion.identity);
                wWall.transform.parent = floor.transform;
                wWall.name = "W Wall";
            }

            if (tile.walls[(int)Direction.EAST])
            {
                GameObject eWall = Instantiate(wallPrefab, new Vector3(xi + 0.5f, xj, -1), Quaternion.identity);
                eWall.transform.parent = floor.transform;
                eWall.name = "E Wall";
            }
        }
    }


}

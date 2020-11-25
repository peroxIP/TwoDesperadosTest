using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public int width;
    public int height;
    public int obsticleCount;

    public GameObject tilePrefab;
    public GameObject wallPrefab;

    private GameObject[,] allTiles;

    private Grid grid;

    void Start()
    {
        PopulateValues();
        allTiles = new GameObject[width, height];
        grid = new Grid(width, height);
        
        InstantiateTiles(ref grid.GetTiles());
    }

    private void InstantiateTiles(ref Tile[,] tiles)
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
                floor.transform.parent = this.transform;


                
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

    private void PopulateValues()
    {
        if(SettingValues.Width != 0) {
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
}

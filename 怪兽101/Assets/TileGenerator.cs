 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
 
/* Danndx 2021 (youtube.com/danndx)
From video: youtu.be/qNZ-0-7WuS8
thanks - delete me! :) */

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject prefab_grass;
    public GameObject prefab_stone;
    public GameObject prefab_water;
    public GameObject prefab_sand;

    int map_width = 160;
    int map_height = 90;

    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

    // recommend 4 to 20
    float magnification = 7.0f;

    // Offset for isometric grid
    public float x_offset = 0;
    public float y_offset = 0;

    // Constants for isometric tile size
    float tileWidth = 1.0f;   // Adjust this value
    float tileHeight = 0.5f;  // Adjust this value

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for (int y = 0; y < map_height; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y);
                noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y);
            }
        }

        // Sort tiles by Y-coordinate to ensure proper layering
        SortTilesByY();
    }

    int GetIdUsingPerlin(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin);
        float scaled_perlin = clamp_perlin * 4; // We assume you have 4 tile types

        return Mathf.FloorToInt(scaled_perlin);
    }

    void CreateTile(int tile_id, int x, int y)
    {
        GameObject tile_prefab = GetTilePrefab(tile_id);
        GameObject tile = Instantiate(tile_prefab);

        // Adjust the position for isometric grid
        float xPos = x * tileWidth - (map_width * tileWidth * 0.5f);
        float yPos = y * tileHeight - (map_height * tileHeight * 0.5f);

        // Adjust tile position to close the gap
        if (y % 2 == 1) // For every odd row
        {
            xPos += tileWidth * 0.5f;
        }

        tile.transform.position = new Vector3(xPos, yPos, y);

        tile_grid[x].Add(tile);
    }

    GameObject GetTilePrefab(int tile_id)
    {
        switch (tile_id)
        {
            case 0:
                return prefab_grass;
            case 1:
                return prefab_stone;
            case 2:
                return prefab_water;
            case 3:
                return prefab_sand;
            default:
                return prefab_grass;
        }
    }

    void SortTilesByY()
    {
        // Sort all tiles by their Y-coordinates to ensure proper layering
        foreach (List<GameObject> column in tile_grid)
        {
            column.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        }
    }
    
    }
}

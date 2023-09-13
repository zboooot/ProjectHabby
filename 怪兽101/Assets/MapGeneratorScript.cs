using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Danndx 2021 (youtube.com/danndx)
From video: youtu.be/qNZ-0-7WuS8
thanks - delete me! :) */

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject prefab_grass;
    public GameObject prefab_stone;
    public GameObject prefab_water;
    public GameObject prefab_sand;

    public int map_width = 160;
    public int map_height = 90;

    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

    // recommend 4 to 20
    public float magnification = 1.0f;

    // Offset for isometric grid
    public float x_offset = 0;
    public float y_offset = 0;

    // Constants for isometric tile size
    public float tileWidth = 1.0f;   // Adjust this value
    public float tileHeight = 0.5f;  // Adjust this value

    // Flag to control map generation
    private bool mapGenerated = false;

    void Start()
    {
        if (!mapGenerated)
        {
            GenerateMap();
            mapGenerated = true; // Set the flag to true to prevent regeneration
            Debug.Log("Generate Map");
        }
    }

    void Update()
    {
        // Check for real-time editing here if needed
        // For example, you can check for keyboard input to modify parameters
        // This part can be customized based on your needs
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Allow regeneration by resetting the flag
            mapGenerated = false;
            Debug.Log("Restart");
            DestroyAndClearMap();
            GenerateMap();
        }

        
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

        // Adjusted thresholds for each tile type, including sand
        float grassThreshold = 0.2f;  // grass threshold
        float stoneThreshold = 0.4f;  // Stone threshold
        float waterThreshold = 0.0f;  // Water threshold
        float sandThreshold = 0.6f;   // Sand threshold


        if (clamp_perlin < grassThreshold)
        {
            return 0; // Grass tile
        }
        else if (clamp_perlin < stoneThreshold)
        {
            return 1; // Stone tile
        }
        else if (clamp_perlin < waterThreshold)
        {
            return 2; // Water tile
        }
        else if (clamp_perlin < sandThreshold)
        {
            return 3; // Sand tile
        }
        else
        {
            return 4; // Additional tile type, if needed
        }
    }

    void CreateTile(int tile_id, int x, int y)
    {
        GameObject tile_prefab = GetTilePrefab(tile_id);

        // Calculate the position in the isometric grid
        float xPos = x * tileWidth - (map_width * tileWidth * 0.5f);
        float yPos = y * tileHeight - (map_height * tileHeight * 0.5f);

        // Adjust tile position for isometric grid
        if (y % 2 == 1) // For every odd row
        {
            xPos += tileWidth * 0.5f;
        }

        // Calculate the tile's height in the isometric grid
        float zPos = x * 0.1f + y * 0.1f; // Adjust the multiplier as needed

        // Instantiate the tile and set its position as a child of this MapGenerator GameObject
        GameObject tile = Instantiate(tile_prefab, transform);
        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(xPos, yPos, zPos);

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


    void DestroyMap()
    {
        foreach (List<GameObject> column in tile_grid)
        {
            foreach (GameObject tile in column)
            {
                Destroy(tile);
            }
            column.Clear();
        }
        tile_grid.Clear();
    }

    // Call this method to clear map data
    void ClearMapData()
    {
        noise_grid.Clear();
        // You can also clear any other data structures related to the map here
    }

    // Example of resetting the generation flag
    void ResetGenerationFlag()
    {
        mapGenerated = false;
    }

    // Example of how to destroy the map and clear data
    void DestroyAndClearMap()
    {
        DestroyMap();
        ClearMapData();
        ResetGenerationFlag(); // Reset the flag to allow regeneration
    }
}
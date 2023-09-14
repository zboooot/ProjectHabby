using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Danndx 2021 (youtube.com/danndx)
From video: youtu.be/qNZ-0-7WuS8
thanks - delete me! :) */

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject prefab_water;
    public GameObject prefab_grass;
    public GameObject prefab_stone;
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

    // Adjusted thresholds for other tile types (grass, stone, sand)
    
    public float size;

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
        // Randomize the Perlin noise seed every time the map generates
        x_offset = Random.Range(0f, 1000f);
        y_offset = Random.Range(0f, 1000f);

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

        float falloff = Falloff(x, y);

        float adjusted_perlin = raw_perlin * falloff;

        float waterThreshold = 0.2f;
        float grassThreshold = 0.4f;
        float stoneThreshold = 0.6f; // Adjust this value for stone
        float sandThreshold = 0.7f;  // Adjust this value for sand

        if (adjusted_perlin < waterThreshold)
        {
            return 0; // water
        }
        else if (adjusted_perlin < grassThreshold)
        {
            return 1; // grass tile
        }
        else if (adjusted_perlin < stoneThreshold)
        {
            return 2; // stone tile
        }
        else if (adjusted_perlin < sandThreshold)
        {
            return 3; // sand tile
        }
        else
        {
            return 0; // water tile
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
                return prefab_water;
            case 1:
                return prefab_grass;
            case 2:
                return prefab_stone;
            case 3:
                return prefab_sand;
            default:
                return prefab_water;
        }
    }



float Falloff(int x, int y)
{
    // Calculate the normalized distance from the center of the map
    float centerX = map_width * 0.5f;
    float centerY = map_height * 0.5f;
    float distanceX = Mathf.Abs(x - centerX) / (map_width * 0.5f);
    float distanceY = Mathf.Abs(y - centerY) / (map_height * 0.5f);

    // Use a maximum of the X and Y distances
    float distanceFromCenter = Mathf.Max(distanceX, distanceY);

    // Define a center radius where you want the falloff to start
    float centerRadius = 1f; // Adjust this value as needed

    // Apply a falloff curve to create less water in the center
    if (distanceFromCenter < centerRadius)
    {
        // You can use a lower exponent to create a gentler falloff
        return Mathf.Clamp01(1 - Mathf.Pow(distanceFromCenter / centerRadius, size));
    }
    else
    {
        // Outside the center radius, no falloff (full value)
        return 0f;
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
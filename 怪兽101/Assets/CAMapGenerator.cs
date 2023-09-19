using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public string seed;
    [Range(0, 100)]
    public int randomFillPercent;
    public bool useRandomSeed;

    public GameObject Ground_prefab;
    public GameObject Water_prefab;
    public GameObject Building1_prefab;
    public GameObject Building2_prefab;
    public GameObject Building3_prefab;



    int[,] map;

    void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        {
            // Destroy previous map objects if they exist
            DestroyMapObjects();

            map = new int[mapWidth, mapHeight];
            RandomFillMap();

            

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Vector3 position = new Vector3(x - mapWidth / 2 + 0.5f, y - mapHeight / 2 + 0.5f, 0);

                    if (map[x, y] == 1)
                    {
                        Instantiate(Water_prefab, position, Quaternion.identity);

                    }
                    else if (map[x, y] == 0)
                    {
                        
                        Instantiate(Ground_prefab, position, Quaternion.identity);

                    }

                }
                for (int i = 0; i < 5; i++)
                {
                    SmoothMap();
                }
            }

            List<Vector3> buildingPositions = new List<Vector3>();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Vector3 position = new Vector3(x - mapWidth / 2 + 0.5f, y - mapHeight / 2 + 0.5f, 0);

                    if (map[x, y] == 0)
                    {
                        bool canPlaceBuilding = IsAreaClearOfBuildings(position, 0.5f);

                        if (canPlaceBuilding && Random.Range(0, 100) < 15)
                        {
                            // Randomly select a building prefab from your available options
                            GameObject buildingPrefab = GetRandomBuildingPrefab();

                            // Instantiate the building prefab at the calculated position
                            Instantiate(buildingPrefab, position, Quaternion.identity);

                            // Store the building position
                            buildingPositions.Add(position);
                        }
                    }
                }
            }
        }


        GameObject GetRandomBuildingPrefab()
        {
            // Randomly select one of your building prefabs
            int randomIndex = Random.Range(0, 3); // Assuming you have 3 building prefabs
            switch (randomIndex)
            {
                case 0:
                    return Building1_prefab;
                case 1:
                    return Building2_prefab;
                case 2:
                    return Building3_prefab;
                default:
                    return Building1_prefab; ; // Default to the first prefab
            }
        }

        void DestroyMapObjects()
        {
            GameObject[] existingMapObjects = GameObject.FindGameObjectsWithTag("MapObject");

            foreach (GameObject obj in existingMapObjects)
            {
                Destroy(obj);
            }

            DestroyBuilding();
        }

        void DestroyBuilding()
        {
            GameObject[] existingBuildingObjects = GameObject.FindGameObjectsWithTag("Building");

            foreach (GameObject obj in existingBuildingObjects)
            {
                Destroy(obj);
            }
        }

        void RandomFillMap()
        {
            if (useRandomSeed)
            {
                seed = Time.time.ToString();
            }

            System.Random pseudoRandom = new System.Random(seed.GetHashCode());

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        // Adjust the randomFillPercent to reduce blue tiles within green tiles
                        map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                    }
                }
            }


        }

        void SmoothMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    // Modify smoothing criteria to reduce blue tiles within green tiles
                    if (neighbourWallTiles > 4)
                    {
                        map[x, y] = 1;
                    }
                    else if (neighbourWallTiles < 4)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }

        bool IsAreaClearOfBuildings(Vector3 position, float minDistance)
        {
            // Find all existing building objects in the scene
            GameObject[] existingBuildings = GameObject.FindGameObjectsWithTag("Building");

            foreach (GameObject building in existingBuildings)
            {
                // Check the distance between the potential building position and existing buildings
                float distance = Vector3.Distance(position, building.transform.position);

                if (distance < minDistance)
                {
                    return false; // Too close to an existing building, can't place
                }
            }

            return true; // No overlap with existing buildings, can place
        }

        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight)
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += map[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }

    }
}

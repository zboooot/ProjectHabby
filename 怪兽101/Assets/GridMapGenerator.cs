using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{

    public List<GameObject> Neighbourhood = new List<GameObject>();
    public int numberOfColumns ; // Number of columns in the grid
    public int numberOfRows;    // Number of rows in the grid

    public float spacingX = 50.0f;   // Horizontal spacing between prefabs
    public float spacingY = 50.0f;   // Vertical spacing between prefabs
    public List<GameObject> obstacleList = new List<GameObject>();
    // Start is called before the first frame update
    public void Start()
    {
        generateMap();
        AstarPath.active.Scan(); //scan the grid
        ScanAndInsert();
        DisableObstacles();
        
    }

    void ScanAndInsert()
    {
        GameObject[] obstacleCollider = GameObject.FindGameObjectsWithTag("Obstacle");
        
        foreach ( var obs in obstacleCollider)
        {
            obstacleList.Add(obs);    
        }
    }

    void generateMap()
    {
        // Create an empty parent object
        GameObject mapParent = new GameObject("MapParent");

        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                // Calculate the index based on the current row and column
                int indexToInstantiate = Random.Range(0, Neighbourhood.Count);

                GameObject prefabToInstantiate = Neighbourhood[indexToInstantiate];

                // Calculate the position based on the row and column
                Vector3 spawnPosition = new Vector3(col * spacingX, row * spacingY, 0f);

                // Instantiate the prefab as a child of the mapParent
                GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                instantiatedObject.transform.parent = mapParent.transform;
            }
        }
    }


    void DisableObstacles()
    {
        foreach (var obs in obstacleList)
        {
            Destroy(obs);
        }

    }
}


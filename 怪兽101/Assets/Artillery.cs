using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : MonoBehaviour
{

    public Camera mainCamera;
    public int numberOfLocations = 10; // Change the number of locations here
    public float minX = -5f; // Adjust these values based on your camera size and desired range
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    public GameObject artilleryPrefab;
    public Transform artilleryPos;
    public float moveSpeed;

    [SerializeField]
    private List<Vector3> objectPositions = new List<Vector3>();

    private void Start()
    {
        StartCoroutine(SpawnArtilleryWithDelay());

        //GenerateRandomLocations(numberOfLocations);
       
    }

    private IEnumerator SpawnArtilleryWithDelay()
    {
        // Calculate the local boundaries of the camera relative to the artillery spawner
        Vector3 localMinBoundary = artilleryPos.InverseTransformPoint(mainCamera.transform.position + new Vector3(minX, minY, 0));
        Vector3 localMaxBoundary = artilleryPos.InverseTransformPoint(mainCamera.transform.position + new Vector3(maxX, maxY, 0));

        for (int i = 0; i < numberOfLocations; i++)
        {
            // Generate random local positions within the boundaries
            float localX = Random.Range(localMinBoundary.x, localMaxBoundary.x);
            float localY = Random.Range(localMinBoundary.y, localMaxBoundary.y);

            // Transform local positions to global positions
            Vector3 randomPosition = artilleryPos.TransformPoint(new Vector3(localX, localY, 0));

            GameObject artillery = Instantiate(artilleryPrefab, artilleryPos.position, Quaternion.identity);
            objectPositions.Add(randomPosition);

            // Calculate the direction from the current position to the target position
            Vector3 moveDirection = (randomPosition - artillery.transform.position).normalized;

            // Calculate the angle in radians
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x);

            // Calculate the rotation in degrees, making the negative Y-axis face the direction
            float rotationAngle = Mathf.Rad2Deg * angle - 90f;

            // Rotate the artillery prefab
            artillery.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            // Start moving the artillery and destroy it when it stops moving
            StartCoroutine(MoveToPosition(artillery.transform, randomPosition));

            // Wait for 0.3 seconds before spawning the next artillery
            yield return new WaitForSeconds(0.3f);
        }
    }


    private IEnumerator MoveToPosition(Transform transform, Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

   

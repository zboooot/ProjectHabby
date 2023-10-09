using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrbManager : MonoBehaviour
{
    public GameObject orbPrefab;  // Prefab for the orb
    public Transform orbSpawnPoint;  // Position to spawn the orb
    private Transform targetUIPosition;  // Target position on the UI canvas
    public float orbFloatSpeed = 1.0f;  // Speed at which the orb floats
    private LevelManager levelManager;

    private void Start()
    {
        targetUIPosition = GameObject.Find("DestructionMeter").GetComponent<Transform>();
        levelManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
    }
    // Call this function when the entity is killed
    public void DropOrbsOnKill()
    {
        // Spawn orbs at the specified spawn point
        GameObject orb = Instantiate(orbPrefab, orbSpawnPoint.position, Quaternion.identity);

        // Move the orb towards the UI target position
        StartCoroutine(MoveOrbToTarget(orb));
    }

    // Coroutine to move the orb towards the UI target position
    private IEnumerator MoveOrbToTarget(GameObject orb)
    {
        Canvas canvas = targetUIPosition.GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas component found in the parent of the targetUIPosition.");
            yield break;
        }

        // Convert UI position to world space
        Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(targetUIPosition.position);

        while (Vector3.Distance(orb.transform.position, targetWorldPosition) > 0.1f)
        {
            // Move the orb towards the target position
            orb.transform.position = Vector3.MoveTowards(orb.transform.position, targetWorldPosition, orbFloatSpeed * Time.deltaTime);
            yield return null;
        }

        levelManager.CalculateScore(1);
        // Destroy the orb once it reaches the target position
        Destroy(orb);
    }
}

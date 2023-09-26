using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreScript : MonoBehaviour
{
    public int entitiesDestroyedCount = 0;
    public GameObject enemyPlane;
    public Animator anim;
    public Transform[] spawnPoints;

    private void Start()
    {
        anim = GameObject.Find("MilitaryAbilityWarning").GetComponent<Animator>();

        // Check if we have at least one spawn point
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
    }


    public void EntityDestroyed()
    {
        entitiesDestroyedCount++;
        Debug.Log("Enemies Destroyed: " + entitiesDestroyedCount);

        if (entitiesDestroyedCount >= 1)
        {
            ActivateSkill();
            Debug.Log("Player can use ultimate skill!");
        }

    }

    public void ActivateSkill()
    {
        if (enemyPlane != null)
        {
            anim.SetBool("Close", true);

            // Randomly choose a spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the GameObject at the desired position
            SpawnObject(randomSpawnPoint);

            Debug.Log("Ultimate skill activated!");
            entitiesDestroyedCount = 0;

        }
    }

    public void SpawnObject(Transform spawnPoint)
    {
        GameObject fighterJet = Instantiate(enemyPlane, spawnPoint.position, spawnPoint.rotation);
        fighterJet.transform.position = new Vector3(fighterJet.transform.position.x, fighterJet.transform.position.y, 0f);
    }

}

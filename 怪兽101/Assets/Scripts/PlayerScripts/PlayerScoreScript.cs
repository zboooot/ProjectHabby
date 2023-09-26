using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreScript : MonoBehaviour
{
    public int entitiesDestroyedCount = 0;
    public GameObject enemyPlane;
    public Animator anim;
    public GameObject spawnPoint;

    private void Start()
    {
        anim = GameObject.Find("MilitaryAbilityWarning").GetComponent<Animator>();
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
    public void OnAnimationEnd()
    {
        //ActivateSkill(); 
    }

    public void ActivateSkill()
    {
        if (enemyPlane != null)
        {
            anim.SetBool("Close", true);

            // Set the Z position to 0 to avoid it being hidden
            Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0f);

            // Instantiate the GameObject at the desired position
            GameObject fighterJet = Instantiate(enemyPlane, spawnPosition, transform.rotation);

            Debug.Log("Ultimate skill activated!");
            entitiesDestroyedCount = 0;

        }
    }

}

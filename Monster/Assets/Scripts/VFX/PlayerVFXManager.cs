using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    public PlayerHandler inputHandler; 
    public GameObject impactSprite;
    public GameObject smokeVFX;
    public void SpawnImpactAtFoot(int footIndex)
    {
        if (footIndex >= 0 && footIndex < inputHandler.legLocations.Length)
        {
            GameObject foot = inputHandler.legLocations[footIndex];
            Vector2 footPos = new Vector2(foot.transform.position.x, foot.transform.position.y);
            Instantiate(impactSprite, footPos, Quaternion.identity);
            Instantiate(smokeVFX, footPos, Quaternion.identity);
        }
    }
}


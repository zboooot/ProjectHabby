using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    public PlayerInputHandler inputHandler; 
    public GameObject impactSprite; 
    public GameObject smokeVFX;

    public void SpawnImpactAtFoot(int footIndex)
    {
        if (footIndex >= 0 && footIndex < inputHandler.feet.Length)
        {
            GameObject foot = inputHandler.feet[footIndex];
            Vector2 footPos = new Vector2(foot.transform.position.x, foot.transform.position.y + 0.8f);
            Instantiate(impactSprite, footPos, Quaternion.identity);
            Instantiate(smokeVFX, footPos, Quaternion.identity);
        }
    }
}


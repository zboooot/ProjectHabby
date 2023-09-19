using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] //Detecting if the player is around
    private float targetDetectionRange = 5;
  
    [SerializeField] //Check if the player is visable to the enemy thus the playerLayerMask
    private LayerMask obstaclesLayerMask, playerLayerMask;
    
    [SerializeField] //Gizmo parameters
    private bool showGizmos = false;

    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        //Find out if player is near
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            //Check if can see player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            //Using the entity postion, the direction of the vector, limited by the targetDetection range, check if theres a obstacle in the way
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);

            //Making sure that the collider seen is on the "Player" layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }

        }
        else
        {
            //Enemy doesn't see the player
            colliders = null;
        }
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach(var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }


}

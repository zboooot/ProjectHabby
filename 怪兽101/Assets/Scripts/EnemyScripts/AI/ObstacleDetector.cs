using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField] //Detect all obstacles in 2 fields of the entity.
    private float detectionRadius = 2;
    
    [SerializeField] //This will be the layermask representing the obstacles.
    private LayerMask layerMask;
     
    [SerializeField] //Where the fresh obstacles will be stored
    private bool showGizmos = true;

    Collider2D[] colliders;
    
    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        aiData.obstacles = colliders;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;
        if (Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach(Collider2D obstacleCollider in colliders)
            {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
            }
        }
    }
}

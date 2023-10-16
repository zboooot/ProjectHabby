using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class CarAI : MonoBehaviour
{
  
    private Vector3 randomDestination;
    public float roamRadius = 20.0f;
    public float roamInterval = 5.0f;
    private Transform player;
    private Vector3 lastPosition;
    private Vector2 movingDirection;

    private SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite destroyedSprite;

    bool isDestroyed;
    Collider2D entityCollider;

    IAstarAI ai;

    void Start()
    {
      
        InvokeRepeating("SetRandomDestination", 0, roamInterval);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        entityCollider = GetComponent<Collider2D>();
        ai = GetComponent<IAstarAI>();
    }

    /*void SetRandomDestination()
    {
            randomDestination = Random.insideUnitSphere * roamRadius;
            randomDestination += transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDestination, out hit, roamRadius, NavMesh.AllAreas))
            {
                if(isDestroyed != true)
                {
                    agent.SetDestination(randomDestination);
                }

                else { agent.updatePosition = false; agent.updateRotation = false; }
            }
    }*/

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * roamRadius;


        point += ai.position;
        return point;
    }
    void FlipSprite(Vector2 movDir)
    {
        if (Mathf.Abs(movDir.x) > Mathf.Abs(movDir.y))
        {
            if (movDir.x > 0)
            {
                spriteRenderer.sprite = rightSprite;
            }
            else
            {
                spriteRenderer.sprite = leftSprite;
            }
        }
        else
        {
            if (movDir.y > 0)
            {
                spriteRenderer.sprite = upSprite;
            }
            else
            {
                spriteRenderer.sprite = downSprite;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLeg")
        {
            Death();
        }
    }

    public void Death()
    {
        isDestroyed = true;
        spriteRenderer.sprite = destroyedSprite;
        entityCollider.enabled = false;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
       // Vector3 currentPosition = transform.position;

       // // Calculate the velocity or position change since the last frame.
       // Vector3 positionChange = currentPosition - lastPosition;

       // // Calculate the moving direction as a normalized vector.
       //// movingDirection = positionChange.normalized;

 
        
       // // Update the last position for the next frame.
       // lastPosition = currentPosition;

       // if(isDestroyed != true)
       // {
       //     FlipSprite(movingDirection);
       // }

       // if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
       // {
       //     ai.destination = PickRandomPoint();
       //     ai.SearchPath();
       // }
    }
}

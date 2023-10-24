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
    private ObjectFadeEffect objectFader;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite destroyedSprite;
    public GameObject smokeVFX;
    public GameObject sparkVFX;
    private Sprite intitialSprite;

    Collider2D entityCollider;

    IAstarAI ai;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        entityCollider = GetComponent<Collider2D>();
        objectFader = GetComponent<ObjectFadeEffect>();
        //ai = GetComponent<IAstarAI>();
        intitialSprite = spriteRenderer.sprite;
    }

    //Vector3 PickRandomPoint()
    //{
    //    var point = Random.insideUnitSphere * roamRadius;


    //    point += ai.position;
    //    return point;
    //}
    //void FlipSprite(Vector2 movDir)
    //{
    //    if (Mathf.Abs(movDir.x) > Mathf.Abs(movDir.y))
    //    {
    //        if (movDir.x > 0)
    //        {
    //            spriteRenderer.sprite = rightSprite;
    //        }
    //        else
    //        {
    //            spriteRenderer.sprite = leftSprite;
    //        }
    //    }
    //    else
    //    {
    //        if (movDir.y > 0)
    //        {
    //            spriteRenderer.sprite = upSprite;
    //        }
    //        else
    //        {
    //            spriteRenderer.sprite = downSprite;
    //        }
    //    }

    //}

    public void SetSpriteRenderer(SpriteRenderer sr)
    {
        spriteRenderer = sr;
    }

    public void SetSpriteUp()
    {
        if (upSprite != null)
        {
            spriteRenderer.sprite = upSprite;
        }
    }
    public void SetSpriteDown()
    {
        if (downSprite != null)
        {
            spriteRenderer.sprite = downSprite;
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
        GameObject smokePuff = Instantiate(smokeVFX, transform.position, Quaternion.identity);
        GameObject sparkPuff = Instantiate(sparkVFX, transform.position, Quaternion.identity);
        spriteRenderer.sprite = destroyedSprite;
        entityCollider.enabled = false;
        objectFader.StartFading();
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
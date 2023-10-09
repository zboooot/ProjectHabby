using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Collider2D coinCollider;

    //GNA Splash Code
    public Transform objTransform;
    private float delay = 0;
    private float pasttime = 0;
    public float when = 1f;
    public float speed;
    private Vector3 off;

    private float detectionRadius = 6f;  // Radius to detect the player
    private float floatSpeed = 7f;  // Speed at which the coin floats towards the player

    private GNAManager gnaManager;
    private Transform player;

    private void Awake()
    {
        //Random value of x
        off = new Vector3(Random.Range(-3, 3), off.y, off.z);
        
        //Random value of y
        off = new Vector3(off.x, Random.Range(-3, 3), off.z);

        gnaManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GNAManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (when >= delay)
        {
            pasttime = Time.deltaTime;
            objTransform.position += off * speed * Time.deltaTime;
            delay += pasttime;
        }

        DetectPlayer();
    }

    void DetectPlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, floatSpeed * Time.deltaTime);
            }
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gnaManager.GainGNA(1);
            Destroy(gameObject);
        }
    }
}

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
    public Transform collector;

    private void Awake()
    {
        //Random value of x
        off = new Vector3(Random.Range(-3, 3), off.y, off.z);
        
        //Random value of y
        off = new Vector3(off.x, Random.Range(-3, 3), off.z);

        gnaManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GNAManager>();
        collector = GameObject.Find("Collector").GetComponent<Transform>();
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
            if (collider.name.Equals("Collector"))
            {
                Vector2 collectPos = new Vector2(collector.transform.position.x, collector.transform.position.y);
                transform.position = Vector3.MoveTowards(transform.position, collectPos, floatSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Collector"))
        {
            gnaManager.GainGNA(1);
            Destroy(gameObject);
        }

    }
    
}

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

    private void Awake()
    {
        //Random value of x
        off = new Vector3(Random.Range(-3, 3), off.y, off.z);
        
        //Random value of y
        off = new Vector3(off.x, Random.Range(-3, 3), off.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(when >= delay)
        {
            pasttime = Time.deltaTime;
            objTransform.position += off * speed * Time.deltaTime;
            delay += pasttime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("GNA gained!");
            Destroy(gameObject);
        }
    }
}

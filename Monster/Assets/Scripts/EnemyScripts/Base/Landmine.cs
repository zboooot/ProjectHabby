using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float radius;
    public float countdown;
    public int damage;
    [SerializeField] private float timer;

    public GameObject explosionVFX;
    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void Explosion()
    {
        //Play explosion VFX & SFX
        Collider2D hitRadius = Physics2D.OverlapCircle(transform.position, radius);
        if (hitRadius.gameObject.CompareTag("Player"))
        {
            PlayerHealthScript playerHp = hitRadius.GetComponent<PlayerHealthScript>();
            playerHp.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(timer > countdown)
            {
                timer += Time.deltaTime;
            }

            if(timer <= countdown)
            {
                Explosion();
                Destroy(gameObject, 2f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer = 0f;
        }
    }
}

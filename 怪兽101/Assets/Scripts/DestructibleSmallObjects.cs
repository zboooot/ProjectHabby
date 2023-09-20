using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSmallObjects : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = destroyedSprite;
            Destroy(gameObject, 5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

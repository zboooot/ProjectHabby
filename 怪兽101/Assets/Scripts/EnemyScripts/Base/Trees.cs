using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;
    private ShakeScript shake;
    private bool isShake;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shake = GetComponent<ShakeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLeg"))
        {
            if(isShake != true)
            {
                shake.StartShake();
                isShake = true;
            }
            spriteRenderer.sprite = destroyedSprite;
        }
    }
}

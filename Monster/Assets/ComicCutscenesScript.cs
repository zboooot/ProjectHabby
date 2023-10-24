using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicCutscenesScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] ComicPages; //An Array
    public int currentPage;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                ChangeSprite();
                if (currentPage == 0)
                {
                    if (Input.touchCount > 0)
                        GoToGame();
                }
            }

        }
        
    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = ComicPages[currentPage];
        currentPage++;
        if (currentPage >= ComicPages.Length)
        {
            currentPage = 0;
        }
    }

    void GoToGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}

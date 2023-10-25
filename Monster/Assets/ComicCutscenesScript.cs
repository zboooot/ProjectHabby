using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicCutscenesScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] ComicPages; //An Array
    public int currentPage;

    public Sprite Pg2;
    public Sprite Pg3;
    public Sprite Pg4;
    public Sprite Pg4Transition;

    public int PgCount;


    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (PgCount == 4)
        {
            GoToGame();
        }
        
    }
    /*void ChangeSprite()
    {
        spriteRenderer.sprite = ComicPages[currentPage];
        currentPage++;
        if (currentPage >= ComicPages.Length)
        {
            currentPage = 0;
        }
    }*/

    public void TurnPage()
    {
        switch(PgCount)
        {
            case 0:
                GetComponent<Image>().sprite = Pg2;
                PgCount++;
                break;
            case 1:
                GetComponent<Image>().sprite = Pg3;
                PgCount++;
                break;
            case 2:
                GetComponent<Image>().sprite = Pg4;
                PgCount++;
                break;
            case 3:
                GetComponent<Image>().sprite = Pg4Transition;
                PgCount++;
                break;
            default:
                break;
        }
    }

    void GoToGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MonsterCardUI;
   
    public void ClosePanel()
    {
        if(MonsterCardUI.activeInHierarchy == false)
        {
            MonsterCardUI.SetActive(true);
        }
        else
        {
            MonsterCardUI.SetActive(false);
        }
    }
}

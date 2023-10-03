using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnclosureScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Monster1InfoCard;

  

   

    public void OnMouseDown()
    {
        Monster1InfoCard.SetActive(true);
    }
}


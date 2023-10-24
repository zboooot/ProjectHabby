using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterScript : MonoBehaviour
{
    private ObjectFadeEffect objectFader;
    void Start()
    {
        objectFader = GetComponent<ObjectFadeEffect>();
        objectFader.StartFading();
    }

}

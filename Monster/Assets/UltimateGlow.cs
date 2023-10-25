using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateGlow : MonoBehaviour
{
    Material PlayerMaterial;
    float thickness;
    public float flickerlength = 100f;
    public UltimateButtonScript ultimateCheck;

    void Start()
    {

        PlayerMaterial = GetComponent<SpriteRenderer>().material;
        thickness = 0f;

    }

    void Update()
    {
        PlayerMaterial.SetFloat("_Thickness", thickness);
        //set property
        if (ultimateCheck.ultimateReady)
        {
            TriggerGlow();
        }

        else
        {
            StopGlow();
        }

    }

    void TriggerGlow()
    {
        if (thickness != 0.005f)
        {
            thickness = Mathf.Lerp(0f, 0.005f, flickerlength);
        }

    }

    void StopGlow()
    {
        if (thickness != 0f)
        {
            thickness = Mathf.Lerp(0.005f, 0f, flickerlength);
        }
    }
}

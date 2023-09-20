using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GNACounter : MonoBehaviour
{
    public static GNACounter instance;

    public TMP_Text GNAText;

    public int StoredGNA = 0;

    public MonsterIncome monsterincome;

     void Awake()
    {
        instance = this;  
    }

     void Start()
    {
        GNAText.text = "GNA:" + StoredGNA.ToString();
    }

    

    public void UpdateIncomeUI()
    {
        // Update the UI text element with the current income.
        GNAText.text = "GNA:" + monsterincome.Storedincome.ToString();

    }
}

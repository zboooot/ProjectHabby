using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GNACounter : MonoBehaviour
{
    public static GNACounter instance;

    public TMP_Text GNAText;

    public int TotalGNA = 0;

    public MonsterIncome monsterincome;

    public PlayerStatScriptableObject monster1;
    public PlayerStatScriptableObject monster2;

    

    void Awake()
    {
        instance = this;
        
    }

     void Start()
    {
        GNAText.text = "GNA:" + TotalGNA.ToString();
    }
    private void Update()
    {
        
    }


    public void UpdateIncomeUI()
    {
        // Update the UI text element with the current income.
        TotalGNA = monster1.Storedincome + monster2.Storedincome;
        GNAText.text = "GNA:" + TotalGNA.ToString();
        Debug.Log("Printthisshit");


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterIncome : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public int incomePerSecond = 1;
    public int currentIncome = 0;
    public int Storedincome = 0;
    private float timer = 0.0f;
    private bool isMining = true;

    // Reference to your UI text element for displaying income.
    public TMP_Text incomeText;
    GNACounter IncomeStorage;

     void Start()
    {
        IncomeStorage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GNACounter>();
    }
    private void Update()
    {
        if (isMining)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                currentIncome += incomePerSecond;
                
                timer = 0.0f;
            }
        }
    }

    public void OnMouseDown()
    {
        // Transfer the stored income to the UI display.
        Storedincome = Storedincome + currentIncome;
        currentIncome = 0;
        isMining = true;
        IncomeStorage.UpdateIncomeUI();

        //Eg this is to increase player health
        //playerData.health += value;


    }

   
}

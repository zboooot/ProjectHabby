using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GNAManager : MonoBehaviour
{

    public ResourceScriptableObject gnaData;
    public TextMeshProUGUI gnaCounter;

    // Start is called before the first frame update
    void Start()
    {
        gnaData.inGameGNA = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gnaCounter.text = "GNA: " + gnaData.inGameGNA;
    }

    public void UpdateTotal()
    {
        gnaData.currentGNA += gnaData.inGameGNA;
    }

    public void GainGNA(int amount)
    {
        gnaData.inGameGNA += amount;
    }
}

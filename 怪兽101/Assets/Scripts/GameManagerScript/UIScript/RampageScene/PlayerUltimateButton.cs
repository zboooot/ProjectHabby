using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateButton : MonoBehaviour
{

    public PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
    }

    public void ActivateUltimate()
    {
        inputHandler.ultimating = true;
    }

}

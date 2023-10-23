using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UltimateButtonScript : MonoBehaviour
{
    public Transform player;
    public PlayerInputHandler inputHandler;
    public PlayerStatScriptableObject playerData;
    public TextMeshProUGUI text;
    public GameObject ultimateMenu;

    bool isSlowed;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
        
        button = GetComponent<Button>();
        ultimateMenu.SetActive(false);
    }

    public void ActivateTime()
    {
        if (!isSlowed)
        {
            Time.timeScale = Mathf.Lerp(1, 0.4f, 5);
            ultimateMenu.SetActive(true);
            isSlowed = true;
        }
    }

    public void DeactivateSlowMo()
    {
        Time.timeScale = Mathf.Lerp(0.4f, 1, 5);
        ultimateMenu.SetActive(false);
    }

    void CheckForActivation()
    {
        if(inputHandler.currentUltimateCharge == playerData.maxUltimateCharge)
        {
            button.interactable = true;
            text.gameObject.SetActive(true);
        }

        else
        {
            button.interactable = false;
            text.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Get the player's position in world space and convert it to screen space
            Vector3 playerPositionScreenSpace = Camera.main.WorldToScreenPoint(player.position);

            // Set the button's position to match the player's screen position
            transform.position = playerPositionScreenSpace;
        }

        CheckForActivation();
    }
}

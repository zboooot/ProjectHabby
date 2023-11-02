using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public GameObject PauseMenu;
	public GameObject PauseButton;
	public LevelManagerScriptableObject levelData;

	public void EnterGame()
	{
		SceneManager.LoadScene("LevelSelectScene");
	}

	public void PauseGame()
	{
		PauseMenu.SetActive(true);
		PauseButton.SetActive(false);
		Time.timeScale = 0;
		Debug.Log("paused");
		
	}

	public void ResumeGame()
	{
		PauseMenu.SetActive(false);
		PauseButton.SetActive(true);
		Time.timeScale = 1;
	}

	public void LoadLab()
    {
		SceneManager.LoadScene("LabScene");
    }

	public void LoadGame()
	{
		if(levelData.cityLevel <= 0 && levelData.cityLevel > 10)
        {
			SceneManager.LoadScene("GameplayScene");
		}
        else
		{
			SceneManager.LoadScene("LandmarkDesScene");
		}
	}

	public void LeaveGame()
    {
		SceneManager.LoadScene("LevelSelectScene");
    }
}

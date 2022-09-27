using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelName;

    public GameObject levelBeatUI;

    private void Update()
    {
        if (PlayerPrefs.HasKey("Game_Beat"))
        {
            if (PlayerPrefs.GetInt("Game_Beat") == 1)
            {
                levelBeatUI.SetActive(true);
            }
            else
            {
                levelBeatUI.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Game_Beat", 0);
            levelBeatUI.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelName);
        Debug.Log("Loading Game Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

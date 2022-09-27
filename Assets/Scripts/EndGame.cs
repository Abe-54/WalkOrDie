using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public string mainMenu;

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reload Scene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Debug.Log("Back To Main Menu");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Game_Beat", 1);
            ReturnToMainMenu();
        }
    }
}

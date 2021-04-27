using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMainGameLevelZero()
    {
        SceneManager.LoadScene("Level0Part1");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("newStart");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quitting");
    }
}

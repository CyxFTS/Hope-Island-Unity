using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMainGameLevelZero()
    {
        SceneManager.LoadScene("level0scene1");
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

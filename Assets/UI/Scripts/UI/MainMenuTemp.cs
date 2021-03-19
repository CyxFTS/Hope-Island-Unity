using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuTemp : MonoBehaviour
{

    public Button[] buttons;

    public void PlayMainGameLevelZero()
    {
        SceneManager.LoadScene("level0scene1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathPromptPanel : MonoBehaviour
{
    public Button m_Restart;
    public void PlayMainMenu()
    {
        SceneManager.LoadScene("level0scene1");
    }
}

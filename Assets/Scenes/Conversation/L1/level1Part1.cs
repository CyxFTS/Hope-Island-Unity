using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1Part1 : MonoBehaviour
{
     public void ClickEvent1()
    {
        Debug.Log("change the scene!!!");
        SceneManager.LoadScene("level1scene1-1");
    }
}

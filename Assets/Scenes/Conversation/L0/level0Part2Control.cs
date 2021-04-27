using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level0Part2Control : MonoBehaviour
{
    void Start()
    {
        // Debug.Log("hello in the part2Control!");
    }
   public void ClickEvent1()
    {
        // Debug.Log("change the scene!!!");
        SceneManager.LoadScene("level1scene1-1");
    }
}

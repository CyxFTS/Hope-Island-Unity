using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class level3Part1 : MonoBehaviour
{
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    public TMP_Text[] playername;

    void Start()
    {
        int idx = ES3.Load("PlayerId", 1);
        currentPlayerName = playerName[idx];
        if(playername!=null){
            for(int i=0;i<playerName.Length;i++){
                playername[i].text = currentPlayerName;
            }
        }
    }
    public void ClickEvent1()
    {
        // Debug.Log("change the scene!!!");
        SceneManager.LoadScene("level3scene1-1");
    }
}

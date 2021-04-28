using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class level2Part2 : MonoBehaviour
{
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    public TMP_Text[] playername;

    void Start()
    {
        int idx = ES3.Load("PlayerId", 1);
        currentPlayerName = playerName[idx];
        if(playername!=null){
            for(int i=0;i<playername.Length;i++){
                playername[i].text = playername[i].text.Replace("<player>", currentPlayerName);
            }
        }
    }
     public void ClickEvent1()
    {
       
        SceneManager.LoadScene("level2scene3-1");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class level1Control : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogs;
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    private int curIndex = 0;

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

    public void ClickEventPart2()
    {
        curIndex++;
        if (curIndex < 3)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }else{
            // SceneManager.LoadScene("level1scene2");
            SceneManager.LoadScene("level1sceneBoss");
        }
        
    }

    public void ClickEventPart3()
    {
        curIndex++;
        if (curIndex < 3)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }else{
            SceneManager.LoadScene("level2Part1");
            
        }

    }



}

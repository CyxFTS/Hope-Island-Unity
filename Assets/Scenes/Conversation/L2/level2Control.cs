using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class level2Control : MonoBehaviour
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

    public void ClickEventPart1()
    {
        curIndex++;
        if (curIndex < 2)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }else{
            SceneManager.LoadScene("level2scene1-1");
        }
    }

    public void ClickEventPart3()
    {
        curIndex++;
        if (curIndex < 5)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }else{
            SceneManager.LoadScene("level3Part1");
        }
    }



}

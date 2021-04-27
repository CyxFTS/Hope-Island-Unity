using UnityEngine;
using UnityEngine.SceneManagement;

public class level0Control : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] dialogs;
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    private int curIndex = 0;

    void Start()
    {
        int idx = ES3.Load("PlayerId", 1);
        currentPlayerName = playerName[idx];
    }

    public void ClickEvent1()
    {
        curIndex++;
        if (curIndex < 8)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        } else
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
    }
    public void ClickEvent2()
    {
        curIndex++;
        if (curIndex < 5)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }else{
            SceneManager.LoadScene("level0scene1");
        }
    }


    
}

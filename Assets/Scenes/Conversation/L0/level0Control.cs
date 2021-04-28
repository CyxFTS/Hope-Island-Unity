using UnityEngine;
using UnityEngine.SceneManagement;

public class level0Control : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] dialogs;
    [SerializeField] private GameObject video;
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
        } 
        else if(curIndex == 8) {
            panels[0].SetActive(false);
            video.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        }
        //else {
            
        //    panels[1].SetActive(true);
        //}
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
    public void ClickEvent3()
    {
        panels[0].SetActive(true);
    }


}

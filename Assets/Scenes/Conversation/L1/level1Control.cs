using UnityEngine;

public class level1Control : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogs;
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    private int curIndex = 0;

    void Start()
    {
        int idx = ES3.Load("PlayerId", 1);
        currentPlayerName = playerName[idx];
    }

    public void ClickEventPart2()
    {
        curIndex++;
        if (curIndex < 3)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }
        
    }

    public void ClickEventPart3()
    {
        curIndex++;
        if (curIndex < 3)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }

    }



}

using UnityEngine;
using UnityEngine.UI;

public class level3Control : MonoBehaviour
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
        if (curIndex < 6)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }
    }

}

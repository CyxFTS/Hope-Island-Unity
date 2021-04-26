using UnityEngine;
using UnityEngine.UI;

public class level3Control2 : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogs;
    [SerializeField] private GameObject[] images;
    private string[] playerName = { "Char", "Marc", "Vincent" };
    private string currentPlayerName = "";
    private int curIndex = 0;

    void Start()
    {
        int idx = ES3.Load("PlayerId", 1);
        currentPlayerName = playerName[idx];
    }

    public void ClickEventPart3()
    {
        curIndex++;
        if (curIndex < 5)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        } else if (curIndex == 5)
        {
            images[0].SetActive(false);
            images[1].SetActive(true);
        } else if (curIndex < 7)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }
    }

}

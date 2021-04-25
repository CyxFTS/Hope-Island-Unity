using UnityEngine;

public class level1Control : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogs;
    private int curIndex = 0;

    public void ClickEvent()
    {
        curIndex++;
        if (curIndex < 7)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }
        
    }



}

using UnityEngine;

public class level2Control : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogs;
    private int curIndex = 0;

    
    public void ClickEvent1()
    {
        curIndex++;
        if (curIndex < 8)
        {
            dialogs[curIndex - 1].SetActive(false);
            dialogs[curIndex].SetActive(true);
        }
    }



}

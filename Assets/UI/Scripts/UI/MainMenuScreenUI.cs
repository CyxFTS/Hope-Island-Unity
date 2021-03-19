using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreenUI : Window
{
    private MainMenuScreenPanel m_MainMenuScreenPanel;

    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_MainMenuScreenPanel = m_GameObject.GetComponent<MainMenuScreenPanel>();
        AddButtonClickListener(m_MainMenuScreenPanel.m_NewGame, () =>
        {

            //SceneManager.LoadScene("level0scene1");
            Debug.Log("½øÈë³¡¾°");
            //TODO ½øÈë³¡¾°
        });
        AddButtonClickListener(m_MainMenuScreenPanel.m_Settings, () =>
        {
            UIManager.Instance.PopUpWnd(PathCollections.UI_Setting);
            //TODO ½øÈëÉèÖÃ
        });
    }
}

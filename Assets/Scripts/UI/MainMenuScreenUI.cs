using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenUI : Window
{
    private MainMenuScreenPanel m_MainMenuScreenPanel;

    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_MainMenuScreenPanel = m_GameObject.GetComponent<MainMenuScreenPanel>();
        AddButtonClickListener(m_MainMenuScreenPanel.m_NewGame, () =>
        {
            
            UIManager.Instance.PopUpWnd(PathCollections.UI_In_GameSreen);
            Debug.Log("进入场景");
            //TODO 进入场景
        });
        AddButtonClickListener(m_MainMenuScreenPanel.m_Settings, () =>
        {
            UIManager.Instance.PopUpWnd(PathCollections.UI_Setting);
            //TODO 进入设置
        });
    }
}

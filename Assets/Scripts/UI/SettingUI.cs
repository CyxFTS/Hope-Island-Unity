using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : Window
{
    SettingPanel m_SettingPanel;
    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_SettingPanel = m_GameObject.GetComponent<SettingPanel>();
        AddButtonClickListener(m_SettingPanel.m_Back, () =>
        {
            UIManager.Instance.PopUpWnd(PathCollections.UI_MainMenuScreen);
            //TODO 返回战斗界面
        });

    }
}

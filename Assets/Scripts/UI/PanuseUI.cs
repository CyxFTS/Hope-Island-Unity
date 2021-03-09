using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanuseUI : Window
{
    PausePanel m_PausePanel;
    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_PausePanel = m_GameObject.GetComponent<PausePanel>();
        AddButtonClickListener(m_PausePanel.m_Back, () =>
        {
            UIManager.Instance.PopUpWnd(PathCollections.UI_In_GameSreen);
            //TODO 返回战斗界面
        });

    }
}

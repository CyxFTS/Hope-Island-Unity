using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class In_GameSreenUI :Window
{
    In_GameSreenPanel m_In_GameSreenPanel;

    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_In_GameSreenPanel = m_GameObject.GetComponent<In_GameSreenPanel>();
        AddButtonClickListener(m_In_GameSreenPanel.m_Pause, () =>
        {
            UIManager.Instance.PopUpWnd(PathCollections.UI_Pause);
            Debug.Log("暂停");
            //TODO 进入暂停
        });
        //m_In_GameSreenPanel.m_ETCJoystick.onMove.AddListener((v2) =>
        //{
        //    Debug.Log($"移动中{v2}");
        //});
    }
}

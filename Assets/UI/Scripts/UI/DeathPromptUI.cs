using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPromptUI : Window
{
    DeathPromptPanel m_DeathPromptPanel;

    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_DeathPromptPanel = m_GameObject.GetComponent<DeathPromptPanel>();
        AddButtonClickListener(m_DeathPromptPanel.m_Restart, () =>
        {
            Debug.Log(" ß∞‹ΩÁ√Ê∑µªÿ");
        });
       
    }
}

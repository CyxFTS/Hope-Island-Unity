using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class UIManager:Singleton<UIManager>
{
    public Font font;
    public static Delegate Delegate;
    private Transform UI_ROOT;
    public Transform m_WndRoot {
        get { return UI_ROOT; }
    }

    //注册
    public Dictionary<string, System.Type> m_RegisterDic = new Dictionary<string, System.Type>();
    //打开的窗口列表
    private List<Window> m_WindowList=new List<Window>();
    //所有打开的窗口
    private Dictionary<string, Window> m_WindowDic = new Dictionary<string, Window>();
    public void Init(Transform parrent)
    {
        UI_ROOT = parrent;
    }

    public void Register<T>(string name) where T : Window
    {
        m_RegisterDic[name] = typeof(T);
    }
    public Window PopUpWnd(string wndName, bool bTop = true, object param1 = null,object param2 = null,object param3 = null)
    {
        Window wnd = FindWndByName<Window>(wndName);
        if (wnd == null)
        {
            System.Type tp = null;
            if (m_RegisterDic.TryGetValue(wndName, out tp))
            {
                wnd = System.Activator.CreateInstance(tp) as Window;
            }
            else
            {
                Debug.LogError("找不到窗口对应的脚本，窗口名是：" + wndName);
                return null;
            }

            for (int i = 0; i < m_WindowList.Count; i++)
            {
                CloseWnd(m_WindowList[i].Name);
            }
            GameObject wndObj = null;
            wndObj = GameObject.Instantiate(Resources.Load<GameObject>(wndName)) as GameObject;
            
            if (wndObj == null)
            {
                Debug.Log("创建窗口Prefab失败：" + wndName);
                return null;
            }
            
            if (!m_WindowDic.ContainsKey(wndName))
            {
                m_WindowDic.Add(wndName, wnd);
                m_WindowList.Add(wnd);
            }
            wndObj.transform.SetAsLastSibling();
           
            wnd.m_GameObject = wndObj;
            wnd.Transform = wndObj.transform;
            wnd.Name = wndName;
            wndObj.transform.SetParent(m_WndRoot, false);
            wnd.Awake(param1, param2, param3);

            wnd.OnShow(param1, param2, param3);
        }
        else
        {
            ShowWnd(wndName, bTop, param1,param2,param3);
            for (int i = 0; i < m_WindowList.Count; i++)
            {
                CloseWnd(m_WindowList[i].Name);
            }
            m_WindowList.Add(FindWndByName<Window>(wndName));
        }
        return wnd;
    }
    
    public void TopWind(string wndName)
    {
        
        
    }
    private void ShowWnd(string name, bool bTop, object param1,object paraml2, object param3)
    {
        Window wnd = FindWndByName<Window>(name);
        ShowWnd(wnd, bTop, param1,paraml2,param3);
    }
    public void ShowWnd(Window wnd, bool bTop = true, object param1 = null, object param2 = null, object param3=null)
    {
        if (wnd != null)
        {
            if (wnd.m_GameObject != null && !wnd.m_GameObject.activeSelf) wnd.m_GameObject.SetActive(true);
            if (bTop) wnd.Transform.SetAsLastSibling();
            wnd.OnShow(param1, param2, param3);
        }
    }

    public void CloseWnd(string windowName, bool destory = false)
    {
        Window window = m_WindowDic[windowName];
        if (window != null)
        {
            m_WindowList.Remove(window);

            if (destory)
            {
                window.OnDisable();
                window.OnClose();

                m_WindowDic.Remove(window.Name);
                GameObject.Destroy(window.m_GameObject);
                window.m_GameObject = null;
            }
            else
            {
                window.m_GameObject.SetActive(false);
            }
        }
    }


    public T FindWndByName<T>(string name) where T : Window
    {
        Window wnd = null;
        if (m_WindowDic.TryGetValue(name, out wnd))
        {
            return (T)wnd;
        }

        return null;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < m_WindowList.Count; i++)
        {
            Window window = m_WindowList[i];
            window?.OnUpdate();
        }
    }
}


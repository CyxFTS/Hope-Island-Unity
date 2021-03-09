using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    void Awake()
    {
        GameLaunch.DontDestroyOnLoad(this.gameObject);
        UIManager.Instance.Init(Instantiate(Resources.Load<GameObject>(PathCollections.RootPath)).transform);
        RegisterUI();
    }
    void Start()
    {
        StartCoroutine(ShowStartScreen());
        
    }

    private IEnumerator ShowStartScreen()
    {
        UIManager.Instance.PopUpWnd(PathCollections.UI_StartScreen);
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.PopUpWnd(PathCollections.UI_MainMenuScreen);
        UIManager.Instance.CloseWnd(PathCollections.UI_StartScreen, true);
    } 
    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.OnUpdate();
    }

    void RegisterUI()
    {
        UIManager.Instance.Register<MainMenuScreenUI>(PathCollections.UI_MainMenuScreen);
        UIManager.Instance.Register<In_GameSreenUI>(PathCollections.UI_In_GameSreen);
        UIManager.Instance.Register<StartScreenUI>(PathCollections.UI_StartScreen);
        UIManager.Instance.Register<SettingUI>(PathCollections.UI_Setting);
        UIManager.Instance.Register<PanuseUI>(PathCollections.UI_Pause);
        UIManager.Instance.Register<MainMenuScreen_savedUI>(PathCollections.UI_MainMenuScreen_saved);
    }
}

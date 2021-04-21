using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class CharacterSelection : MonoBehaviour
{
    public GameObject player;
    
    public void SelectCharacterOne()
    {
        player.GetComponent<PlayerSkills>().PlayerId = 1;
        LoadNextScene();
    }
    
    public void SelectCharacterTwo()
    {
        player.GetComponent<PlayerSkills>().PlayerId = 2;
        LoadNextScene();
    }
    
    public void SelectCharacterThree()
    {
        player.GetComponent<PlayerSkills>().PlayerId = 3;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        var random = new System.Random();
        var randomScene = random.Next(1, 5);
        var sceneToLoad = "level1scene1-" + randomScene;
        player.GetComponent<PlayerController>().SavePlayerSaveData();
        SceneManager.LoadScene(sceneToLoad);
    }
}

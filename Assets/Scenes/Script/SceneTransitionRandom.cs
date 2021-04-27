using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionRandom : MonoBehaviour
{
    public int currentLevel;
    public Boolean next;
    public GameObject player;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            int nextLevel = currentLevel + 1;
            if (!next) nextLevel -= 1;
            System.Random random = new System.Random();
            int randomScene;
            String sceneToLoad;
            if (nextLevel == 2)
            {
                randomScene = random.Next(1, 3);
                sceneToLoad = "level" + nextLevel + "scene3-" + randomScene;
            }
            else if (nextLevel == 3)
            {
                randomScene = random.Next(1, 3);
                sceneToLoad = "level" + nextLevel + "scene2-" + randomScene;
            }
            else
            {
                randomScene = random.Next(1, 5);
                sceneToLoad = "level" + nextLevel + "scene1-" + randomScene;
            }
            Debug.Log(sceneToLoad);
            player.GetComponent<PlayerController>().SavePlayerSaveData();
            SceneManager.LoadScene(sceneToLoad);
            // Victory
        }
    }
}

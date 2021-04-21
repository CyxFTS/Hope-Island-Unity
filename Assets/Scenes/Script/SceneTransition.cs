using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject player;
    public Boolean selectCharacter;
    public GameObject characterSelector;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (selectCharacter)
            {
                characterSelector.SetActive(true);
            }
            else
            {
                player.GetComponent<PlayerController>().SavePlayerSaveData();
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}

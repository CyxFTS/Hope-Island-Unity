using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOne : MonoBehaviour
{
    public GameObject sceneTransition;
    private GameObject[] _enemies;
    
    private void Start()
    {
        sceneTransition.SetActive(false);
    }

    private void Update()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies.Length == 0)
        {
            sceneTransition.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOne : MonoBehaviour
{
    public GameObject sceneTransition;
    private GameObject[] _enemies;
    private GameObject[] _bosses;
    
    private void Start()
    {
        sceneTransition.SetActive(false);
    }

    private void Update()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (_enemies.Length + _bosses.Length == 0)
        {
            sceneTransition.SetActive(true);
        }
    }
}

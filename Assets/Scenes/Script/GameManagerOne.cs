using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOne : MonoBehaviour
{
    public GameObject sceneTransition;
    public GameObject player;
    public bool setHp = false;
    private GameObject[] _enemies;
    private GameObject[] _bosses;
    
    private void Start()
    {
        sceneTransition.SetActive(false);
    }

    private void Update()
    {
        if (setHp) { player.GetComponent<PlayerController>().SetHP(100); setHp = false; }
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (_enemies.Length + _bosses.Length == 0)
        {
            sceneTransition.SetActive(true);
        }
    }
}

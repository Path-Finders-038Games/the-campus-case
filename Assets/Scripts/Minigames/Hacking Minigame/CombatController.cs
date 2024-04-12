using System;
using Minigames.Hacking_Minigame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    //object which will be shot
    public GameObject Bullet_Prefab;

    //time between bullets firing
    public float ReloadTime;

    // variable which keeps track of the time
    private float _timer;

    private int _fireDelayTracker = 0;
    
    // Update is called once per frame
    private void Update()
    {
        //only execute it the game is played
        if (!GameController.gameController.PlayingGame) return;

        // add time that has passed to the time tracker variable
        _timer += Time.deltaTime;

        //if passed time exceeded the reloadtime, shoot a new bullet
        if (!(_timer > ReloadTime)) return;
        
        //reset the timer and instantiate a new bullet
        _timer = 0;
        _fireDelayTracker++;

        if (_fireDelayTracker < 2) return; // Give the game to play animation
            
        Instantiate(Bullet_Prefab, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

    }
}

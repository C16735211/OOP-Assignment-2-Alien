﻿using UnityEngine;
using System.Collections;

public class BGCollector : MonoBehaviour {

    // collect background and deactive during the game
    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Background" ) {
            target.gameObject.SetActive(false);
        }
        
    }

} // BGCollector

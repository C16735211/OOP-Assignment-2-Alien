using UnityEngine;
using System.Collections;

public class CloudCollector : MonoBehaviour {

    // function to collect the clouds when it collides as it goes down screen
     void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Cloud" || target.tag == "Deadly")
        {
            target.gameObject.SetActive(false);
        }
    }

} // CloudCollector

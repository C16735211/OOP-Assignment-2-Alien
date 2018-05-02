using UnityEngine;
using System.Collections;

public class CloudCollector : MonoBehaviour {

    // Function gets called when objects collide or touch
    // Triggers when another gameobject collides or touches our cloud collector
 
     void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Cloud" || target.tag == "Deadly")
        {
            // deactivate gameobject in the scene
            target.gameObject.SetActive(false);
        }
    }

} // CloudCollector

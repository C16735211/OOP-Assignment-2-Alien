using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour {

    private float minX, maxX;

	// Use this for initialization
	void Start () {
        SetMinAndMax();

    }
	
	// Update is called once per frame
	void Update () {

        // checks if player is outside of the bounds
        // don't let the player go out of bounds on the left
	    if(transform.position.x < minX) {
            Vector3 temp = transform.position;
            temp.x = minX;
            transform.position = temp;
        }


        // checks if player is outside of the bounds
        // don't let the player go out of bounds on the right
        if (transform.position.x > maxX) {
            Vector3 temp = transform.position;
            temp.x = maxX;
            transform.position = temp;
        }
	}

    void SetMinAndMax () {

        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // set the values of the players out 
        // of bounds position can tweek if necessary 
        // e.g. 0.5f, -0.5f;
        maxX = bounds.x;
        minX = -bounds.x;

    }


} // PlayerBounds

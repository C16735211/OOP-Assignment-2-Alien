using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour {

	
	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        // width of sprite
        float width = sr.sprite.bounds.size.x;

        // set up the world width and height of game 
        // in the camera area relevant to screen
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight / Screen.height * Screen.width;

        tempScale.x = worldWidth / width;

        transform.localScale = tempScale;

	}
	
	
} // BGScaler

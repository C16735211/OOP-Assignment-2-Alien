using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // control maximum velocity of the player
    public float speed = 8f, maxVelocity = 4f;

    // move player and animation on player
    private Rigidbody2D myBody;
    private Animator anim;

    // Awake function we inherit from MonoBehaviour
    // Apply rigidbody to player prefab
    // Apply animation to player prefab
    void Awake () {
        myBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
    // FixedUpdate calls every 3rd or 4th frame
    // B Function to deal with physics
	void FixedUpdate () {
        PlayerMoveKeyboard();
	}

    // Function to move player using keyboard
    void PlayerMoveKeyboard () {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        // utilize A, D, up, down, left or right arrow keys to -1 or 1 , 0 if not pressed to 
        // move the player
        float h = Input.GetAxisRaw("Horizontal");

        // move player right
        if(h > 0) {
            if (vel < maxVelocity)
                forceX = speed;

            // makes reference to the player facing right 
            Vector3 temp = transform.localScale;
            temp.x = 0.6f;
            transform.localScale = temp;

            // walk parameter set to true 
            anim.SetBool("Walk", true);

            // go to left
        } else if(h < 0) {

            if (vel < maxVelocity)
                forceX = -speed;

            // makes reference to player turning and facing left
            Vector3 temp = transform.localScale;
            temp.x = -0.6f;
            transform.localScale = temp;

            // walk parameter set walk to be true
            anim.SetBool("Walk", true);
        } else {
            // else idle, stop walking
            anim.SetBool("Walk", false);
        }

        // apply to rigidbody
        myBody.AddForce(new Vector2(forceX, 0));
                

    }

} // Player

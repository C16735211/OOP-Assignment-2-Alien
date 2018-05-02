using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    [SerializeField]
    private AudioClip coinClip, lifeClip;

    private CameraScript cameraScript;

    private Vector3 previousPosition;
    private bool countScore;

    public static int scoreCount;
    public static int lifeCount;
    public static int coinCount;

    // reference to camera script
    void Awake () {
        cameraScript = Camera.main.GetComponent<CameraScript> ();
    }



    // Use this for initialization
    void Start () {
        previousPosition = transform.position;
        countScore = true;

	}
	
	// Update is called once per frame
	void Update () {
        CountScore();
	}

    // when player moves down successfully score increments 
    void CountScore() {
        if(countScore) {
            if(transform.position.y < previousPosition.y) {
                scoreCount++;
            }
            previousPosition = transform.position;
            GameplayController.instance.SetScore(scoreCount);
        }
    }

    // function that when player touches coin, life, darkcloud or bounds
    // coin = score count
    // life add life if life < 2
    // darkcloud = player dies
    // bounds = player dies
    void OnTriggerEnter2D(Collider2D target) {
        
        if (target.tag == "Coin") {
            coinCount++;
            scoreCount += 200;

            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetCoinScore(coinCount);

            // play coin clip when player collects a coin
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);

        }

        if (target.tag == "Life") {
            lifeCount++;
            scoreCount += 300;

            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetLifeScore(lifeCount);

            // play lifeClip when player collects a heart
            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if (target.tag == "Bounds" || target.tag == "Deadly") {
            cameraScript.moveCamera = false;
            countScore = false;

            GameplayController.instance.GameOverShowPanel(scoreCount, coinCount);

            // not important moves player out of sight of camera
            // because he died
            transform.position = new Vector3(500, 500, 0);
            lifeCount--;
            GameManager.instance.CheckGameStatus (scoreCount, coinCount, lifeCount);
        }

        /*
        if (target.tag == "Deadly") {
            cameraScript.moveCamera = false;
            countScore = false;

            GameplayController.instance.GameOverShowPanel(scoreCount, coinCount);

            transform.position = new Vector3(500, 500, 0);
            lifeCount--;
        }
        */


    }

} // PlayerScore git add

using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

    // other classes cannot access the variable/gameobject
    [SerializeField] 
    private GameObject[] clouds;

    private float distanceBetweenClouds = 3f;

    private float minX, maxX;

    private float lastCloudPositionY;

    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    void Awake () {
        controlX = 0;
        SetMinAndMax ();
        CreateClouds ();
        player = GameObject.Find("Player");

        for(int i = 0; i < collectables.Length; i++) {
            collectables[i].SetActive(false);
        }
    }

    void Start () {
        PositionThePlayer();
    }

    // World position in the scene in unity
    // Screen coordinates converted into world coordinates in Unity
  
    void SetMinAndMax () {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // position clouds inside the width of screen/
        // background on both sides
        maxX = bounds.x - 0.5f;
        minX = -bounds.x + 0.5f;
    }

    // Function to shuffle a random collection of gameobjects that move position
    // randomise positions every new game
    void Shuffle(GameObject[] arrayToShuffle) {
        for(int i = 0; i < arrayToShuffle.Length; i++) {
            GameObject temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    // position clouds in the game
    void CreateClouds() {

        Shuffle(clouds);

        float positionY = 0f;

        // 
        for (int i = 0; i < clouds.Length; i++) {

            // position of cloud at element [i]
            Vector3 temp = clouds[i].transform.position;

            // last position in cloud[i] on y axis
            temp.y = positionY;

            // position the clouds on the x axis
            if (controlX == 0) {
                temp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            } else if (controlX == 1) {
                temp.x = Random.Range(0.0f, minX);
                controlX = 2;
            } else if (controlX == 2) {
                temp.x = Random.Range(1.0f, maxX);
                controlX = 3;
            } else if (controlX == 3) {
                temp.x = Random.Range(-1.0f, minX);
                controlX = 0;
            }

            lastCloudPositionY = positionY;

            clouds[i].transform.position = temp;

            positionY -= distanceBetweenClouds;

        }
    }

    // 
    void PositionThePlayer () {

        // Get the reference to all our clouds in the game : Tags 
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        // swap dark cloud position to make sure its not the first cloud the player encounters
        for(int i = 0; i < darkClouds.Length; i++) {

            if(darkClouds[i].transform.position.y == 0f) {

                Vector3 t = darkClouds[i].transform.position;

                // how the player doesn't face having dark cloud(s) when starting the game
                // reposition dark clouds in a suitable position in the scene
                darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
                                                               cloudsInGame[0].transform.position.y,
                                                               cloudsInGame[0].transform.position.z);

                cloudsInGame[0].transform.position = t;

            }
        }

        // Position player at the start on a white cloud
        Vector3 temp = cloudsInGame[0].transform.position;

        // comparing player position and reassign player to first cloud position
        for (int i = 1; i < cloudsInGame.Length; i++)
        {
            if (temp.y < cloudsInGame[i].transform.position.y) {
                temp = cloudsInGame[i].transform.position;
            }
        }

        // position player on the  first cloud every time
        temp.y += 0.8f;

        player.transform.position = temp;

    }

    // Respawn new clouds when we hit the last cloud position
    void OnTriggerEnter2D(Collider2D target) {

        if(target.tag == "Cloud" || target.tag == "Deadly") {

            if(target.transform.position.y == lastCloudPositionY) {

                // shuffle position and randomize
                // clouds and collectables
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 temp = target.transform.position;

                for(int i = 0; i < clouds.Length; i++) {

                    // if element at index[i] in the hierarchy not active
                    if(!clouds[i].activeInHierarchy) {

                        if (controlX == 0)
                        {
                            temp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            temp.x = Random.Range(0.0f, minX);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            temp.x = Random.Range(1.0f, maxX);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            temp.x = Random.Range(-1.0f, minX);
                            controlX = 0;
                        }

                        temp.y -= distanceBetweenClouds;

                        // last cloud in the games position
                        // clouds respawn when touched
                        lastCloudPositionY = temp.y;

                        // set and activate new respawned clouds
                        clouds[i].transform.position = temp;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);

                        if (clouds[i].tag != "Deadly") {

                            if(!collectables[random].activeInHierarchy) {

                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;

                                if(collectables[random].tag == "Life") {

                                    if(PlayerScore.lifeCount < 2) {
                                        collectables[random].transform.position = temp2;
                                        collectables[random].SetActive(true);
                                    }

                                } else {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }

                    }

                }

            }

        }

    }

  
} // CloudSpawner

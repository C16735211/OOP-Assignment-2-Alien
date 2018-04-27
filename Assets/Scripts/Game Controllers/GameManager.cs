using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

    [HideInInspector]
    public int score, coinScore, lifeScore;

    void Awake () {
        MakeSingleton ();
    }

    void Start () {
        InitializeVariables ();
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore) {

        if (lifeScore < 0) {

            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = false;

            GameplayController.instance.GameOverShowPanel(score, coinScore);

            if (GamePreferences.GetEasyDifficultyState() == 1) {

                int highScore = GamePreferences.GetEasyDifficultyHighscore();
                int coinHighScore = GamePreferences.GetEasyDifficultyCoinScore();

                if(highScore < score) 
                    GamePreferences.SetEasyDifficultyHighscore(score);

                if (coinHighScore < coinScore)
                    GamePreferences.SetEasyDifficultyCoinScore(coinScore);
            }

            if (GamePreferences.GetMediumDifficultyState() == 1)
            {

                int highScore = GamePreferences.GetMediumDifficultyHighscore();
                int coinHighScore = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score)
                    GamePreferences.SetMediumDifficultyHighscore(score);

                if (coinHighScore < coinScore)
                    GamePreferences.SetMediumDifficultyCoinScore(coinScore);
            }

            if (GamePreferences.GetHardDifficultyState() == 1)
            {

                int highScore = GamePreferences.GetHardDifficultyHighscore();
                int coinHighScore = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score)
                    GamePreferences.SetHardDifficultyHighscore(score);

                if (coinHighScore < coinScore)
                    GamePreferences.SetHardDifficultyCoinScore(coinScore);
            }

           

        } else {

            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;

            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetCoinScore(coinScore);
            GameplayController.instance.SetLifeScore(lifeScore);


            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = true;

            GameplayController.instance.PlayerDiedRestartTheGame ();
        }
    }

    /*
    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Gameplay")
        {

            if (gameRestartedAfterPlayerDied)
            {
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetLifeScore(lifeScore);
                GameplayController.instance.SetCoinScore(coinScore);

                PlayerScore.coinCount = coinScore;
                PlayerScore.scoreCount = score;
                PlayerScore.lifeCount = lifeScore;

            }
            else if (gameStartedFromMainMenu)
            {
                PlayerScore.coinCount = 0;
                PlayerScore.scoreCount = 0;
                PlayerScore.lifeCount = 2;

                GameplayController.instance.SetScore(0);
                GameplayController.instance.SetLifeScore(2);
                GameplayController.instance.SetCoinScore(0);

            }

        }
    }

    */

    void InitializeVariables () {

        if(!PlayerPrefs.HasKey ("Game Initialized")) {

            GamePreferences.SetEasyDifficultyState (0);
            GamePreferences.SetEasyDifficultyCoinScore(0);
            GamePreferences.SetEasyDifficultyHighscore(0);


            GamePreferences.SetMediumDifficultyState(1);
            GamePreferences.SetMediumDifficultyCoinScore(0);
            GamePreferences.SetMediumDifficultyHighscore(0);


            GamePreferences.SetHardDifficultyState(0);
            GamePreferences.SetHardDifficultyCoinScore(0);
            GamePreferences.SetHardDifficultyHighscore(0);

            GamePreferences.SetMusicState(0);

            //  stops the above values resetting to zero
            //  saves the key 
            PlayerPrefs.SetInt("Game Initialized", 123);

        }
    }

    // Carries instance of GameManager through each scene
    // Destroys any duplicates of GameManager also using Singleton Pattern
    // Singleton pattern does not allow multiple copies of the same game object
    // destroy the duplicate of the game object

    void MakeSingleton () {
        if (instance != null) { 
            Destroy (gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
      
    }


} // GameManager

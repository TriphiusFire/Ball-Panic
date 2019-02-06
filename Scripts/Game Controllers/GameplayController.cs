using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private GameObject[] topAndBottomBricks, leftBricks, rightBricks;

    public GameObject panelBG, levelFinishedPanel, playerDiedPanel, pausePanel;

    private GameObject topBrick, bottomBrick, leftBrick, rightBrick;

    private Vector3 coordinates;

    [SerializeField]
    private GameObject[] players;

    public float levelTime = 500;

    public Text liveText, scoreText, coinText, levelTimerText, showScoreAtTheEndOfLevelText, coundDownAndBeginLevelText, watchVideoText;

    private float countDownBeforeLevelBegins = 3.0f;

    public static int smallBallsCount = 0;

    public int playerLives, playerScore, coins;

    public bool isGamePaused, hasLevelBegan, levelInProgress, countdownLevel;

    [SerializeField]
    private GameObject[] endOfLevelRewards;

    [SerializeField]
    private Button pauseBtn;

    private void Awake()
    {
        //Debug.Log("GameplayController.Awake() called");
        CreateInstance();
        InitializeBricksAndPlayer();
    }

    private void Start()
    {
        //Debug.Log("GameplayController.Start() called");
        InitializeGameplayController();
    }

    private void Update()
    {
        //Debug.Log("GameplayController.Update() called");
        UpdateGameplayController();
    }

    void CreateInstance()
    {
        //Debug.Log("GameplayController.CreateInstance() called");
        if (instance == null)
        {
            instance = this;
        }
    }

    void InitializeBricksAndPlayer()
    {
        //Debug.Log("GameplayController.InitializeBricksAndPlayer() called");
        coordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        int index = Random.Range(0, topAndBottomBricks.Length);

        topBrick = Instantiate(topAndBottomBricks[index]);
        bottomBrick = Instantiate(topAndBottomBricks[index]);
        leftBrick = Instantiate(leftBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
        rightBrick = Instantiate(rightBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0,90))) as GameObject;

        topBrick.tag = "TopBrick";
        bottomBrick.tag = "BottomBrick";

        topBrick.transform.position = new Vector3(-coordinates.x, coordinates.y, 0);
        bottomBrick.transform.position = new Vector3(-coordinates.x, -coordinates.y , 0);
        leftBrick.transform.position = new Vector3(-coordinates.x - 0.1f, coordinates.y, 0);
        rightBrick.transform.position = new Vector3(coordinates.x + 0.1f, coordinates.y-10f, 0);

        Instantiate(players[GameController.instance.selectedPlayer]);
    }

    void InitializeGameplayController()
    {
        //Debug.Log("GameplayController.InitializeGameplayController() called");
        if (GameController.instance.isGameStartedFromLevelMenu)
        {
            playerScore = 0;
            playerLives = 2;
            GameController.instance.currentScore = playerScore;
            GameController.instance.currentLives = playerLives;
            GameController.instance.isGameStartedFromLevelMenu = false;
        }
        else
        {
            playerScore = GameController.instance.currentScore;
            playerLives = GameController.instance.currentLives;
        }
        levelTimerText.text = levelTime.ToString("F0");
        scoreText.text = "Score x" + playerScore;
        liveText.text = "x" + playerLives;

        Time.timeScale = 0;
        coundDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");


    }
    
    void UpdateGameplayController()
    {
        //Debug.Log("GameplayController.UpdateGameplayController() called");
        scoreText.text = "Score x" + playerScore;
        liveText.text = "x" + playerLives;
        coinText.text = "x" + coins;

        if (hasLevelBegan)
        {
            CountdownAndBeginLevel();
        }

        if (countdownLevel)
        {
            LevelCountdownTimer();
        }
    }

    public void setHasLevelBegan(bool hasLevelBegan)
    {
        //Debug.Log("GameplayController.setHasLevelBegan() called");
        this.hasLevelBegan = hasLevelBegan;
    }

    void CountdownAndBeginLevel()
    {
        //Debug.Log("GameplayController.CountdownAndBeginLevel() called");
        countDownBeforeLevelBegins -= (0.19f * 0.15f);
        coundDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
        if (countDownBeforeLevelBegins <= 0)
        {
            Time.timeScale = 1f;
            hasLevelBegan = false;
            levelInProgress = true;
            countdownLevel = true;
            coundDownAndBeginLevelText.gameObject.SetActive(false);
        }
    }

    void LevelCountdownTimer()
    {
        //Debug.Log("GameplayController.LevelCountdownTimer() called");
        if (Time.timeScale == 1)
        {
            levelTime -= Time.deltaTime;
            levelTimerText.text = levelTime.ToString("F0");

            if(levelTime <= 0)
            {
                
                GameController.instance.currentLives = playerLives;
                GameController.instance.currentScore = playerScore;

                if(playerLives < 0)
                {
                    StartCoroutine(PromptTheUserToWatchAVideo());
                }
                else
                {
                    StartCoroutine(PlayerDiedRestartLevel());
                }

            }
        }
    }

    IEnumerator PlayerDiedRestartLevel()
    {
        //Debug.Log("GameplayController.PlayerDiedRestartLevel() called");
        levelInProgress = false;

        coins = 0;
        smallBallsCount = 0;

        Time.timeScale = 0;

        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.FadeOut();
        }
        

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.PlayFadeInAnimation();
        }
    }

    public void PlayerDied()
    {
        //Debug.Log("GameplayController.PlayerDied() called");
        countdownLevel = false;
        pauseBtn.interactable = false;
        levelInProgress = false;

        smallBallsCount = 0;

        playerLives--;
        GameController.instance.currentLives = playerLives;

        GameController.instance.currentScore = playerScore;

        if (playerLives < 0)
        {
            StartCoroutine(PromptTheUserToWatchAVideo());
        }
        else
        {
            StartCoroutine(PlayerDiedRestartLevel());
        }
    }

    IEnumerator PromptTheUserToWatchAVideo()
    {
        //Debug.Log("GameplayController.PromptTheUserToWatchAVideo() called");
        levelInProgress = false;
        countdownLevel = false;
        pauseBtn.interactable = false;

        Time.timeScale = 0;

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f));

        playerDiedPanel.SetActive(true);
    }

    IEnumerator LevelCompleted()
    {
        //Debug.Log("GameplayController.LevelCompleted() called");
        countdownLevel = false;
        pauseBtn.interactable = false;

        int unlockedLevel = GameController.instance.currentLevel;

        unlockedLevel++;
        if (!(unlockedLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.levels[unlockedLevel] = true;            
        }

        Instantiate(endOfLevelRewards[GameController.instance.currentLevel],
            new Vector3(0, Camera.main.orthographicSize, 0), Quaternion.identity);

        if (GameController.instance.doubleCoins)
        {
            coins *= 2;
        }

        GameController.instance.coins += coins;
        GameController.instance.Save();

        yield return null;
      

    }

    public void CountSmallBalls()
    {
        //Debug.Log("GameplayController.CountSmallBalls() called");
        if(!GameObject.FindGameObjectWithTag("SmallestBall")
            && !GameObject.FindGameObjectWithTag("SmallBall")
            && !GameObject.FindGameObjectWithTag("MediumBall")
            && !GameObject.FindGameObjectWithTag("LargeBall")
            && !GameObject.FindGameObjectWithTag("LargestBall")) StartCoroutine(LevelCompleted());
        
    }

    public void GoToMapButton()
    {
        //Debug.Log("GameplayController.GoToMapButton() called");
        GameController.instance.currentScore = playerScore;

        if (GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene("LevelMenu");

        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.PlayLoadingScreen();
        }
    }

    public void RestartCurrentLevelButton()
    {
        //Debug.Log("GameplayController.RestartCurrentLevelButton() called");
        smallBallsCount = 0;
        coins = 0;
        GameController.instance.currentLives = playerLives;
        GameController.instance.currentScore = playerScore;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.PlayLoadingScreen();
        }
    }

    public void NextLevel()
    {
        //Debug.Log("GameplayController.NextLevel() called");
        GameController.instance.currentScore = playerScore;
        GameController.instance.currentLives = playerLives;

        if(GameController.instance.highScore< GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        int nextLevel = GameController.instance.currentLevel;
        nextLevel++;

        if (!(nextLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.currentLevel = nextLevel;
            SceneManager.LoadScene("Level" + nextLevel);

            if(LoadingScript.instance != null)
            {
                LoadingScript.instance.PlayLoadingScreen();
            }
        }
    }

    public void PauseGame()
    {
        //Debug.Log("GameplayController.PauseGame() called");
        if (!hasLevelBegan)
        {
            if (levelInProgress)
            {
                if (!isGamePaused)
                {
                    countdownLevel = false;
                    levelInProgress = false;
                    isGamePaused = true;

                    panelBG.SetActive(true);
                    pausePanel.SetActive(true);

                    Time.timeScale = 0;
                }
            }
        }
    }

    public void ResumeGame()
    {
        //Debug.Log("GameplayController.ResumeGame() called");
        countdownLevel = true;
        levelInProgress = true;
        isGamePaused = false;
        panelBG.SetActive(false);
        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
    {
        //Debug.Log("GameplayController.GivePlayerLivesRewardAfterWatchingVideo() called");
        watchVideoText.text = "Thank You For Viewing!~";
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));

        coins = 0;
        playerLives = 2;
        smallBallsCount = 0;
        GameController.instance.currentLevel = playerLives;
        GameController.instance.currentScore = playerScore;
        Time.timeScale = 0;

        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.FadeOut();
        }
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.PlayFadeInAnimation();
        }

    }

    public void DontWatchVideoInsteadQuit()
    {
        //Debug.Log("GameplayController.DontWatchVideoInsteadQuit() called");
        GameController.instance.currentScore = playerScore;

        if(GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }
        Time.timeScale = 1;

        SceneManager.LoadScene("LevelMenu");
        if (LoadingScript.instance != null)
        {
            LoadingScript.instance.PlayLoadingScreen();
        }
    }

}

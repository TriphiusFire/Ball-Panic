using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelController : MonoBehaviour
{
    public Text scoreText, coinText;

    public bool[] levels;

    public Button[] levelButtons;

    public Text[] levelText;

    public Image[] lockIcons;

    public GameObject coinShopPanel;



    // Start is called before the first frame update
    void Start()
    {

        InitializeLevelMenu();
    }

    void InitializeLevelMenu()
    {
        scoreText.text = "" + GameController.instance.highScore;
        coinText.text = "" + GameController.instance.coins;

        levels = GameController.instance.levels;

        for(int i = 1; i < levels.Length; i++)
        {
            if (levels[i])
            {
                lockIcons[i - 1].gameObject.SetActive(false);
            }
            else
            {
                levelButtons[i - 1].interactable = false;
                levelText[i - 1].gameObject.SetActive(false);
            }
        }
    }

    public void LoadLevel()
    {
        if (GameController.instance.isMusicOn)
        {
            MusicController.instance.GameIsLoadedTurnOffMusic();
        }

        string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        GameController.instance.currentLevel = Int32.Parse(level.Substring(5));

        LoadingScript.instance.PlayLoadingScreen();

        //switch (level) // used Int32.Parse instead
        //{
        //    case "Level0":
        //        GameController.instance.currentLevel = 0;
        //        //play loading screen
        //        break;
        //}

        LoadingScript.instance.PlayLoadingScreen();
        GameController.instance.isGameStartedFromLevelMenu = true;
        SceneManager.LoadScene(level);

        //SceneManager.LoadScene("Level Setup Scene");
    }

    public void OpenCoinShop()
    {
        coinShopPanel.SetActive(true);
    }

    public void CloseCoinShop()
    {
        coinShopPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("PlayerMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameData data;

    public int currentLevel = -1; //what level we are on right now
    public int currentScore; //track current score
    public int currentLives;

    public bool isGameStartedFromLevelMenu;


    public bool isGameStartedFirstTime; //initialization if not true

    public bool isMusicOn;

    public bool doubleCoins;

    public int selectedPlayer;
    public int selectedWeapon;
    public int coins;
    public int highScore;

    public bool[] players; //keep track which player locked or unlocked
    public bool[] levels; //
    public bool[] weapons;
    public bool[] achievements;
    public bool[] collectedItems;


    private void Awake()
    {
        MakeSingleton();
        InitializeGameVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void InitializeGameVariables()
    {
        Load();

        if(data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            highScore = 0;
            coins = 0;

            selectedPlayer = 0;
            selectedWeapon = 0;

            isGameStartedFirstTime = false;
            isMusicOn = false;

            players = new bool[6];
            levels = new bool[40];
            weapons = new bool[4];
            achievements = new bool[8];
            collectedItems = new bool[40];

            players[0] = true;
            for(int i = 1; i < players.Length; i++)
            {
                players[i] = false;
            }

            levels[0] = true;
            for(int i = 1; i < levels.Length; i++)
            {
                levels[i] = false;
            }

            weapons[0] = true;
            for(int i = 1; i < weapons.Length; i++)
            {
                weapons[i] = false;
            }

            for(int i = 1; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }
            
            for(int i = 0; i < collectedItems.Length; i++)
            {
                collectedItems[i] = false;
            }
            data = new GameData();

            data.setHighScore(highScore);
            data.setCoins(coins);
            data.setSelectedPlayer(selectedPlayer);
            data.setSelectedWeapon(selectedWeapon);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setPlayers(players);
            data.setLevels(levels);
            data.setWeapons(weapons);
            data.setAchievements(achievements);
            data.setCollectedItems(collectedItems);

            Save();

            Load();
        }
        else
        {
            highScore = data.getHighScore();
            coins = data.getCoins();
            selectedPlayer = data.getSelectedPlayer();
            selectedWeapon = data.getSelectedWeapon();
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            players = data.getPlayers();
            levels = data.getLevels();
            weapons = data.getWeapons();
            achievements = data.getAchievements();
            collectedItems = data.getCollectedItems();
        }
    }

    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if(data != null)
            {
                data.setHighScore(highScore);
                data.setCoins(coins);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setPlayers(players);
                data.setLevels(levels);
                data.setWeapons(weapons);
                data.setSelectedPlayer(selectedPlayer);
                data.setSelectedWeapon(selectedWeapon);
                data.setIsMusicOn(isMusicOn);
                data.setAchievements(achievements);
                data.setCollectedItems(collectedItems);

                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat",FileMode.Open);

            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e)
        {

        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

}//GameController

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime; //initialization if not true

    private bool isMusicOn;

    private bool doubleCoins;

    private int selectedPlayer;
    private int selectedWeapon;
    private int coins;
    private int highScore;

    private bool[] players; //keep track which player locked or unlocked
    private bool[] levels; //
    private bool[] weapons;
    private bool[] achievements;
    private bool[] collectedItems;

    public void setIsGameStartedFirstTime(bool a)
    {
        isGameStartedFirstTime = a;
    }
    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }
    public void setIsMusicOn(bool a)
    {
        isMusicOn = a;
    }
    public bool getIsMusicOn()
    {
        return isMusicOn;
    }
    public void setDoubleCoins(bool a)
    {
        doubleCoins = a;
    }
    public bool getDoubleCoins()
    {
        return doubleCoins;
    }
    public void setSelectedPlayer(int a)
    {
        selectedPlayer = a;
    }
    public int getSelectedPlayer()
    {
        return selectedPlayer;
    }
    public void setSelectedWeapon(int a)
    {
        selectedWeapon = a;
    }
    public int getSelectedWeapon()
    {
        return selectedWeapon;
    }
    public void setCoins(int a)
    {
        coins = a;
    }
    public int getCoins()
    {
        return coins;
    }
    public void setHighScore(int a)
    {
        highScore = a;
    }
    public int getHighScore()
    {
        return highScore;
    }
    public void setPlayers(bool[] a)
    {
        players = a;
    }
    public bool[] getPlayers()
    {
        return players;
    }
    public void setLevels(bool[] a)
    {
        levels = a;
    }
    public bool[] getLevels()
    {
        return levels;
    }
    public void setWeapons(bool[] a)
    {
        weapons = a;
    }
    public bool[] getWeapons()
    {
        return weapons;
    }
    public void setAchievements(bool[] a)
    {
        achievements = a;
    }
    public bool[] getAchievements()
    {
        return achievements;
    }
    public void setCollectedItems(bool[] a)
    {
        collectedItems = a;
    }
    public bool[] getCollectedItems()
    {
        return collectedItems;
    }
}

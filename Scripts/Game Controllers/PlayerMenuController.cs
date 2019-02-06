using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{
    public Text scoreText, coinText;

    public bool[] players;

    public bool[] weapons;

    public Image[] priceTags;

    public Image[] weaponIcons;

    public Sprite[] weaponArrows;

    public int selectedWeapon;

    public int selectedPlayer;

    public GameObject buyPlayerPanel;

    public Button yesBtn, noBtn;

    public Text buyPlayerText;

    public GameObject coinShop;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerMenuController();
    }

    void InitializePlayerMenuController()
    {
        scoreText.text = "" + GameController.instance.highScore;
        coinText.text = "" + GameController.instance.coins;

        players = GameController.instance.players;
        weapons = GameController.instance.weapons;
        selectedPlayer = GameController.instance.selectedPlayer;
        selectedWeapon = GameController.instance.selectedWeapon;

        for(int i = 0; i < weaponIcons.Length; i++)
        {
            weaponIcons[i].gameObject.SetActive(false);
        }

        for(int i = 1; i < players.Length; i++)
        {
            if(players[i] == true)
            {
                priceTags[i - 1].gameObject.SetActive(false);
            }
        }

        weaponIcons[selectedPlayer].gameObject.SetActive(true);
        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
    }

    public void Player1Button()
    {
        if (selectedPlayer != 0)
        {
            selectedPlayer = 0;
            selectedWeapon = 0;

            weaponIcons[selectedPlayer].gameObject.SetActive(true);
            weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

            for (int i = 0; i < weaponIcons.Length; i++)
            {
                if (i == selectedPlayer)
                {
                    continue; //skip player
                }
                weaponIcons[i].gameObject.SetActive(false);

            }
            GameController.instance.selectedPlayer = selectedPlayer;
            GameController.instance.selectedWeapon = selectedWeapon;
            GameController.instance.Save();
        }
        else
        {
            selectedWeapon++;
            if (selectedWeapon == weapons.Length)
            {
                selectedWeapon = 0;
            }
            bool foundWeapon = true;
            while (foundWeapon)
            {
                if (weapons[selectedWeapon] == true)
                {
                    weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                    GameController.instance.selectedWeapon = selectedWeapon;
                    GameController.instance.Save();
                    foundWeapon = false;
                }
                else
                {
                    selectedWeapon++;
                    if (selectedWeapon == weapons.Length)
                    {
                        selectedWeapon = 0;
                    }
                }
            }
        }
    }

    public void PirateButton()
    {
        if (players[1])
        {
            if (selectedPlayer != 1)
            {
                selectedPlayer = 1;
                selectedWeapon = 0;

                weaponIcons[selectedPlayer].gameObject.SetActive(true);
                weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

                for (int i = 0; i < weaponIcons.Length; i++)
                {
                    if (i == selectedPlayer)
                    {
                        continue; //skip player
                    }
                    weaponIcons[i].gameObject.SetActive(false);

                }
                GameController.instance.selectedPlayer = selectedPlayer;
                GameController.instance.selectedWeapon = selectedWeapon;
                GameController.instance.Save();
            }
            else
            {
                selectedWeapon++;
                if (selectedWeapon == weapons.Length)
                {
                    selectedWeapon = 0;
                }
                bool foundWeapon = true;
                while (foundWeapon)
                {
                    if (weapons[selectedWeapon] == true)
                    {
                        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                        GameController.instance.selectedWeapon = selectedWeapon;
                        GameController.instance.Save();
                        foundWeapon = false;
                    }
                    else
                    {
                        selectedWeapon++;
                        if (selectedWeapon == weapons.Length)
                        {
                            selectedWeapon = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if(GameController.instance.coins >= 10000)
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "Purchase This Player?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyPlayer(1));
            }
            else
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "You Need More Coins! Buy Them Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void ZombieButton()
    {
        if (players[2])
        {
            if (selectedPlayer != 2)
            {
                selectedPlayer = 2;
                selectedWeapon = 0;

                weaponIcons[selectedPlayer].gameObject.SetActive(true);
                weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

                for (int i = 0; i < weaponIcons.Length; i++)
                {
                    if (i == selectedPlayer)
                    {
                        continue; //skip player
                    }
                    weaponIcons[i].gameObject.SetActive(false);

                }
                GameController.instance.selectedPlayer = selectedPlayer;
                GameController.instance.selectedWeapon = selectedWeapon;
                GameController.instance.Save();
            }
            else
            {
                selectedWeapon++;
                if (selectedWeapon == weapons.Length)
                {
                    selectedWeapon = 0;
                }
                bool foundWeapon = true;
                while (foundWeapon)
                {
                    if (weapons[selectedWeapon] == true)
                    {
                        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                        GameController.instance.selectedWeapon = selectedWeapon;
                        GameController.instance.Save();
                        foundWeapon = false;
                    }
                    else
                    {
                        selectedWeapon++;
                        if (selectedWeapon == weapons.Length)
                        {
                            selectedWeapon = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if (GameController.instance.coins >= 50000)
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "Purchase This Player?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyPlayer(2));
            }
            else
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "You Need More Coins! Buy Them Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void HomosapienButton()
    {
        if (players[3])
        {
            if (selectedPlayer != 3)
            {
                selectedPlayer = 3;
                selectedWeapon = 0;

                weaponIcons[selectedPlayer].gameObject.SetActive(true);
                weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

                for (int i = 0; i < weaponIcons.Length; i++)
                {
                    if (i == selectedPlayer)
                    {
                        continue; //skip player
                    }
                    weaponIcons[i].gameObject.SetActive(false);

                }
                GameController.instance.selectedPlayer = selectedPlayer;
                GameController.instance.selectedWeapon = selectedWeapon;
                GameController.instance.Save();
            }
            else
            {
                selectedWeapon++;
                if (selectedWeapon == weapons.Length)
                {
                    selectedWeapon = 0;
                }
                bool foundWeapon = true;
                while (foundWeapon)
                {
                    if (weapons[selectedWeapon] == true)
                    {
                        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                        GameController.instance.selectedWeapon = selectedWeapon;
                        GameController.instance.Save();
                        foundWeapon = false;
                    }
                    else
                    {
                        selectedWeapon++;
                        if (selectedWeapon == weapons.Length)
                        {
                            selectedWeapon = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if (GameController.instance.coins >= 250000)
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "Purchase This Player?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyPlayer(3));
            }
            else
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "You Need More Coins! Buy Them Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void JokerButton()
    {
        if (players[4])
        {
            if (selectedPlayer != 4)
            {
                selectedPlayer = 4;
                selectedWeapon = 0;

                weaponIcons[selectedPlayer].gameObject.SetActive(true);
                weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

                for (int i = 0; i < weaponIcons.Length; i++)
                {
                    if (i == selectedPlayer)
                    {
                        continue; //skip player
                    }
                    weaponIcons[i].gameObject.SetActive(false);

                }
                GameController.instance.selectedPlayer = selectedPlayer;
                GameController.instance.selectedWeapon = selectedWeapon;
                GameController.instance.Save();
            }
            else
            {
                selectedWeapon++;
                if (selectedWeapon == weapons.Length)
                {
                    selectedWeapon = 0;
                }
                bool foundWeapon = true;
                while (foundWeapon)
                {
                    if (weapons[selectedWeapon] == true)
                    {
                        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                        GameController.instance.selectedWeapon = selectedWeapon;
                        GameController.instance.Save();
                        foundWeapon = false;
                    }
                    else
                    {
                        selectedWeapon++;
                        if (selectedWeapon == weapons.Length)
                        {
                            selectedWeapon = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if (GameController.instance.coins >= 1000000)
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "Purchase This Player?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyPlayer(4));
            }
            else
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "You Need More Coins! Buy Them Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void SpartanButton()
    {
        if(players[5])
        {
            if (selectedPlayer != 5)
            {
                selectedPlayer = 5;
                selectedWeapon = 0;

                weaponIcons[selectedPlayer].gameObject.SetActive(true);
                weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

                for (int i = 0; i < weaponIcons.Length; i++)
                {
                    if (i == selectedPlayer)
                    {
                        continue; //skip player
                    }
                    weaponIcons[i].gameObject.SetActive(false);

                }
                GameController.instance.selectedPlayer = selectedPlayer;
                GameController.instance.selectedWeapon = selectedWeapon;
                GameController.instance.Save();
            }
            else
            {
                selectedWeapon++;
                if (selectedWeapon == weapons.Length)
                {
                    selectedWeapon = 0;
                }
                bool foundWeapon = true;
                while (foundWeapon)
                {
                    if (weapons[selectedWeapon] == true)
                    {
                        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                        GameController.instance.selectedWeapon = selectedWeapon;
                        GameController.instance.Save();
                        foundWeapon = false;
                    }
                    else
                    {
                        selectedWeapon++;
                        if (selectedWeapon == weapons.Length)
                        {
                            selectedWeapon = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if (GameController.instance.coins >= 10000000)
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "Purchase This Player?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyPlayer(5));
            }
            else
            {
                buyPlayerPanel.SetActive(true);
                buyPlayerText.text = "You Need More Coins! Buy Them Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyPlayer(int index)
    {
        GameController.instance.players[index] = true;
        switch(index)
        {
            case 1:
                GameController.instance.coins -= 10000;
                break;
            case 2:
                GameController.instance.coins -= 50000;
                break;
            case 3:
                GameController.instance.coins -= 250000;
                break;
            case 4:
                GameController.instance.coins -= 1000000;
                break;
            case 5:
                GameController.instance.coins -= 10000000;
                break;
        }
        
        GameController.instance.Save();
        InitializePlayerMenuController();

        buyPlayerPanel.SetActive(false);
    }

    public void OpenCoinShop()
    {
        if (buyPlayerPanel.activeInHierarchy)
        {
            buyPlayerPanel.SetActive(false);
        }
        coinShop.SetActive(true);
    }

    public void CloseCoinShop()
    {
        coinShop.SetActive(false);
    }

    public void DontBuyPlayerOrCoins()
    {
        buyPlayerPanel.SetActive(false);
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

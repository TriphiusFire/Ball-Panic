using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController instance;

    public Text coinText, scoreText, buyArrowsText, watchVideoText;

    public Button weaponsTabBtn, specialTabBtn, earnCoinsTabBtn, yesBtn;

    public GameObject weaponItemsPanel, specialItemsPanel, earnCoinItemsPanel, coinShopPanel, buyArrowsPanel;

    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeShopMenuController();   
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void BuyDoubleArrows()
    {
        Debug.Log("Attempting to Buy DoubleArrows");
        if (!GameController.instance.weapons[1])
        {
            if(GameController.instance.coins >= 10000)
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "Purchase Double Arrows?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(()=>BuyArrow(1));
            }
            else
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "You Need More Coins! Purchase Coins Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyStickyArrow()
    {
        if (!GameController.instance.weapons[2])
        {
            if (GameController.instance.coins >= 20000)
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "Purchase Sticky Arrow?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyArrow(2));
            }
            else
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "You Need More Coins! Purchase Coins Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyDoubleStickyArrows()
    {
        if (!GameController.instance.weapons[3])
        {
            if (GameController.instance.coins >= 30000)
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "Purchase Double Sticky Arrows?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => BuyArrow(3));
            }
            else
            {
                buyArrowsPanel.SetActive(true);
                buyArrowsText.text = "You Need More Coins! Purchase Coins Now?!";
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }

    public void BuyArrow(int index)
    {
        GameController.instance.weapons[index] = true;
        switch (index)
        {
            case 1:
                GameController.instance.coins -= 10000;
                break;
            case 2:
                GameController.instance.coins -= 20000;
                break;
            case 3:
                GameController.instance.coins -= 30000;
                break;
        }
        GameController.instance.Save();

        buyArrowsPanel.SetActive(false);
        coinText.text = "" + GameController.instance.coins;
    }

    void InitializeShopMenuController()
    {
        coinText.text = "" + GameController.instance.coins;
        scoreText.text = "" + GameController.instance.highScore;
    }

    public void OpenCoinShop()
    {
        if (buyArrowsPanel.activeInHierarchy)
        {
            buyArrowsPanel.SetActive(false);
        }
        coinShopPanel.SetActive(true);
    }

    public void CloseCoinShop()
    {
        coinShopPanel.SetActive(false);
    }

    public void OpenWeaponItemsPanel()
    {
        weaponItemsPanel.SetActive(true);
        specialItemsPanel.SetActive(false);
        earnCoinItemsPanel.SetActive(false);
    }

    public void OpenSpecialItemsPanel()
    {
        weaponItemsPanel.SetActive(false);
        specialItemsPanel.SetActive(true);
        earnCoinItemsPanel.SetActive(false);
    }

    public void OpenEarnCoinsItemsPanel()
    {
        weaponItemsPanel.SetActive(false);
        specialItemsPanel.SetActive(false);
        earnCoinItemsPanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DontBuyArrows()
    {
        buyArrowsPanel.SetActive(false);
    }
}

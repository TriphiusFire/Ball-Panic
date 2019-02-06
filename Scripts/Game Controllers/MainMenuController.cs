using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Animator settingsButtonsAnim;

    private bool hidden;
    private bool canTouchSettingsButton;

    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite[] musicBtnSprites;

    [SerializeField]
    private Button fbBtn;

    [SerializeField]
    private Sprite[] fbSprites;

    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private Image infoImage;

    [SerializeField]
    private Sprite[] infoSprites;

    private int infoIndex;



    // Start is called before the first frame update
    void Start()
    {
        canTouchSettingsButton = true;
        hidden = true;

        if (GameController.instance.isMusicOn)
        {
            MusicController.instance.PlayBgMusic();
            musicBtn.image.sprite = musicBtnSprites[0];
        }
        else
        {
            MusicController.instance.StopBgMusic();
            musicBtn.image.sprite = musicBtnSprites[1];
        }

        infoIndex = 0;
        infoImage.sprite = infoSprites[infoIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingsButton()
    {
        StartCoroutine(DisableSettingsButtonWhilePlayingAnimation());
    }

    IEnumerator DisableSettingsButtonWhilePlayingAnimation()
    {
        if (canTouchSettingsButton)//let the animation finish before being able to tap the settings button again
        {
            if (hidden)//not showing yet, so tapping causes buttons to slide in
            {
                canTouchSettingsButton = false;
                settingsButtonsAnim.Play("SlideIn");
                hidden = false;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true;
            }
            else
            {
                canTouchSettingsButton = false;
                settingsButtonsAnim.Play("SlideOut");
                hidden = true;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true;
            }
        }
    }

    public void MusicButton()
    {
        if (GameController.instance.isMusicOn)
        {
            musicBtn.image.sprite = musicBtnSprites[1];
            MusicController.instance.StopBgMusic();
            GameController.instance.isMusicOn = false;
            GameController.instance.Save();
        }
        else
        {
            musicBtn.image.sprite = musicBtnSprites[0];
            MusicController.instance.PlayBgMusic();
            GameController.instance.isMusicOn = true;
            GameController.instance.Save();
        }
    }

    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void NextInfo()
    {
        infoIndex++;
        if(infoIndex == infoSprites.Length)
        {
            infoIndex = 0;
        }

        infoImage.sprite = infoSprites[infoIndex];
    }

    public void PreviousInfo()
    {
        infoIndex--;
        if (infoIndex == -1)
        {
            infoIndex = infoSprites.Length-1;
        }

        infoImage.sprite = infoSprites[infoIndex];
    }

    public void PlayButton()
    {
        MusicController.instance.PlayClickClip();
        SceneManager.LoadScene("PlayerMenu");
    }

    public void ShopButton()
    {
        MusicController.instance.PlayClickClip();
        SceneManager.LoadScene("ShopMenu");
    }


}

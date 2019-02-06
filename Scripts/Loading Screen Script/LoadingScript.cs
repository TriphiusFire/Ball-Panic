using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public static LoadingScript instance;

    [SerializeField]
    GameObject bgImage, logoImage, text;

    [SerializeField]
    GameObject fadePanel;

    [SerializeField]
    Animator fadeAnim;

    private void Awake()
    {
        MakeSingleton();
        Hide();
    }

    void MakeSingleton()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
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

    public void PlayLoadingScreen()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    public void PlayFadeInAnimation()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.4f));

        if(GameplayController.instance != null)
        {
            GameplayController.instance.setHasLevelBegan(true);
        }
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.9f));
        fadePanel.SetActive(false);
    }
    public void FadeOut()
    {
        fadePanel.SetActive(true);
        fadeAnim.Play("FadeOut");
    }

    IEnumerator ShowLoadingScreen()
    {
        Show();
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        Hide();

        //call gameplaycontroller to begin game
        if (GameplayController.instance != null)
        {
            GameplayController.instance.setHasLevelBegan(true);
        }
    }

    void Show()
    {
        bgImage.SetActive(true);
        logoImage.SetActive(true);
        text.SetActive(true);
    }

    void Hide()
    {
        bgImage.SetActive(false);
        logoImage.SetActive(false);
        text.SetActive(false);
    }
}

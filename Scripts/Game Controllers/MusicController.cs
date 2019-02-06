using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    private AudioSource bgMusic, click;

    private float time;
    
   
    void Awake()
    {
        MakeSingleton();

        AudioSource[] audioSources = GetComponents<AudioSource>();

        bgMusic = audioSources[0];
        click = audioSources[1];
    }

    void MakeSingleton()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "LevelMenu")
        {
            if (GameController.instance.isMusicOn)
            {
                if (!bgMusic.isPlaying)
                {
                    bgMusic.time = time;
                    bgMusic.Play();
                }
            }
        }
    }

    public void GameIsLoadedTurnOffMusic()
    {
        if (bgMusic.isPlaying)
        {
            time = bgMusic.time;  //continue where we left off next time
            bgMusic.Stop();
        }
    }

    public void PlayBgMusic()
    {
        if (!bgMusic.isPlaying)
        {
            bgMusic.Play();
        }
    }

    public void StopBgMusic()
    {
        if (bgMusic.isPlaying)
        {
            bgMusic.Stop();
        }
    }
    
    public void PlayClickClip()
    {
        click.Play();
    }

}// MusicController

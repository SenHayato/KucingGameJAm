using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public GameObject canvas;
    public ItemManager itemmanager;
    public GameObject retrybutton;
    public GameObject nextlevelbutton;

    public AudioClip retrysfx;
    public AudioClip menusfx;
    public AudioClip nextlevelsfx;
    public AudioSource sourcesfx;
    public AudioSource bgm;

    [SerializeField]
    public List<AudioClip> winsfx;
    public List<AudioClip> losesfx;


    public void Awake()
    {
        itemmanager.onGameWin += OnGameWin;
        itemmanager.onGameOver += OnGameOver;
        canvas.SetActive(false);    
       bgm.mute = false;
    }

    private void OnGameWin()
    {
        canvas.SetActive(true);
        retrybutton.SetActive(false);
        nextlevelbutton.SetActive(true);
        sourcesfx.PlayOneShot(winsfx[UnityEngine.Random.Range(0, winsfx.Count - 1)]);
        bgm.mute = true;
    }

    private void OnGameOver()
    {
        canvas.SetActive(true);
        retrybutton.SetActive(true);
        nextlevelbutton.SetActive(false);
        sourcesfx.PlayOneShot(losesfx[UnityEngine.Random.Range(0, losesfx.Count - 1)]);
        bgm.mute = true;
    }

    public void BackMenu(string Title)
    {
        sourcesfx.PlayOneShot(menusfx);
        Invoke(nameof(menudelay), .5f);
    }

    public void Restart(string GameScene)
    {
        sourcesfx.PlayOneShot(retrysfx);
        Invoke(nameof(restartdelay), .5f);
    }

    public void NextLevel(string Level)
    {
        sourcesfx.PlayOneShot(nextlevelsfx);
        bgm.mute = false;
        canvas.SetActive(false);
        sourcesfx.Stop();
    }

    public void restartdelay()
    {
        SceneManager.LoadScene("GameScene 1");
        
    }

    public void menudelay()
    {
        SceneManager.LoadScene("Title");

    }
}

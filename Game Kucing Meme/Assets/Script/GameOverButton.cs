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

    public void Awake()
    {
        itemmanager.onGameWin += OnGameWin;
        itemmanager.onGameOver += OnGameOver;
        canvas.SetActive(false);    
    }

    private void OnGameWin()
    {
        canvas.SetActive(true);
        retrybutton.SetActive(false);
        nextlevelbutton.SetActive(true);
    }

    private void OnGameOver()
    {
        canvas.SetActive(true);
        retrybutton.SetActive(true);
        nextlevelbutton.SetActive(false);

    }

    public void BackMenu(string Title)
    {
        SceneManager.LoadScene("Title");
    }

    public void Restart(string GameScene)
    {
        SceneManager.LoadScene("GameScene 1");
    }

    public void NextLevel(string Level)
    {
        canvas.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    public ItemManager manager;
    public Button buttonNextLevel;
    public int addItemAmount = 3;

    private void Awake()
    {
        manager.onGameOver += OnGameOver;
        manager.onGameWin += OnGameWin;
        buttonNextLevel.onClick.AddListener(OnNextLevel);
    }

    private void OnNextLevel()
    {
        manager.currentLevel += addItemAmount;
        manager.ShowCurrentLevelItem();
    }

    private void OnGameOver()
    {
        Debug.Log("GAME OVER");
    }

    private void OnGameWin()
    {
        Debug.Log("GAME WIN");
    }
}

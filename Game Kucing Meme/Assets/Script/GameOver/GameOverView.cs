using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    public ItemManager manager;

    private void Awake()
    {
        manager.onGameOver += OnGameOver;
        manager.onGameWin += OnGameWin;
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

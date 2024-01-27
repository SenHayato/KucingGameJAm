using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour
{
    public GameObject canvas;
    public ItemManager itemmanager;
    public GameObject retrybutton;
    public GameObject nextlevelbutton;
    public GameObject pop;

    public AudioClip retrysfx;
    public AudioClip menusfx;
    public AudioClip nextlevelsfx;
    public AudioSource sourcesfx;
    public AudioSource bgm;

    [SerializeField]
    public List<AudioClip> winsfx;
    public List<AudioClip> losesfx;

    [SerializeField]
    public List<GameObject> winsprite;
    public int currentWinSpriteIndex;
    public List<Sprite> losesprite;

    public Image winmeme;
    public Image losememe;

    public ScoreScript skorpause;

    public void Awake()
    {
        itemmanager.onGameWin += OnGameWin;
        itemmanager.onGameOver += OnGameOver;
        canvas.SetActive(false);    
       bgm.mute = false;
        Time.timeScale = 1;
        //InvokeRepeating(nameof(popAnim), 1, 1);
        foreach (var item in winsprite)
        {
            item.SetActive(false);
        }
    }

    private void OnGameWin()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
        retrybutton.SetActive(false);
        nextlevelbutton.SetActive(true);
        sourcesfx.PlayOneShot(winsfx[UnityEngine.Random.Range(0, winsfx.Count)]);
        bgm.mute = true;
        winsprite[currentWinSpriteIndex].SetActive(true);
        winsprite[currentWinSpriteIndex].GetComponent<Animation>().Play();
        currentWinSpriteIndex = (currentWinSpriteIndex < winsprite.Count) ? currentWinSpriteIndex : 0;
        winmeme.enabled = true;
        losememe.enabled = false;
        
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
        retrybutton.SetActive(true);
        nextlevelbutton.SetActive(false);
        sourcesfx.PlayOneShot(losesfx[UnityEngine.Random.Range(0, losesfx.Count)]);
        bgm.mute = true;
        losememe.sprite = losesprite[UnityEngine.Random.Range(0, losesprite.Count)];
        losememe.enabled = true;
        winsprite[currentWinSpriteIndex].SetActive(false);
      
    }

    public void BackMenu(string Title)
    {
        sourcesfx.PlayOneShot(menusfx);
        Invoke(nameof(menudelay), .5f);
        Time.timeScale = 1;
    }

    public void Restart(string GameScene)
    {
        Time.timeScale = 1;
        sourcesfx.PlayOneShot(retrysfx);
        Invoke(nameof(restartdelay), .5f);
    }

    public void NextLevel(string Level)
    {
        Time.timeScale = 1;
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

    //private int currentPopAnimIndex;

    /*public void popAnim()
    {        
        winmeme.sprite = winsprite[currentPopAnimIndex];
        currentPopAnimIndex = (currentPopAnimIndex < winsprite.Count) ? currentPopAnimIndex : 0;
    }*/
}

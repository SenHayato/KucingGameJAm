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
    public GameObject winAnn;
    public GameObject loseAnn;

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
        canvas.SetActive(true);
        retrybutton.SetActive(false);
        nextlevelbutton.SetActive(true);
        bgm.mute = true;
        sourcesfx.loop =true;
        foreach (var item in winsprite)
            item.SetActive(false);
        currentWinSpriteIndex = UnityEngine.Random.Range(0, winsprite.Count);
        winsprite[currentWinSpriteIndex].SetActive(true);
        sourcesfx.clip = winsfx[currentWinSpriteIndex];
        sourcesfx.Play();
        winmeme.enabled = true;
        losememe.enabled = false;
        winAnn.SetActive(true);
        loseAnn.SetActive(false);
        
    }

    private void OnGameOver()
    {
        canvas.SetActive(true);
        retrybutton.SetActive(true);
        nextlevelbutton.SetActive(false);
        int currentLoseAssetIndex = UnityEngine.Random.Range(0, losesprite.Count);
        sourcesfx.PlayOneShot(losesfx[currentLoseAssetIndex]);
        bgm.mute = true;
        losememe.sprite = losesprite[currentLoseAssetIndex];
        losememe.enabled = true;
        foreach (var item in winsprite)
            item.SetActive(false);
        winAnn.SetActive(false);
        loseAnn.SetActive(true);
      
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
        sourcesfx.loop = false;
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

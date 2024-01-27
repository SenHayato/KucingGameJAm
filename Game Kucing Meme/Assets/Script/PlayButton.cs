using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioSource playsfx;
    public void LoadScene(string GameScene)
    {
        playsfx.PlayOneShot(playsfx.clip);
        Invoke(nameof(loadelay), .5f);
    }

    public void loadelay()
    {
        SceneManager.LoadScene("GameScene 1");

    }

}

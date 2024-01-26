using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void LoadScene(string GameScene)
    {
        SceneManager.LoadScene("GameScene");
    }

}

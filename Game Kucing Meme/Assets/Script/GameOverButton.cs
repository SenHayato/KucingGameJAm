using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void BackMenu(string Title)
    {
        SceneManager.LoadScene("Title");
    }

    public void Restart(string GameScene)
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatFeature : MonoBehaviour
{
    public ItemManager manager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Title");

        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.DeleteKey("HS");

        if (Input.GetKeyDown(KeyCode.Equals))
            manager.itemFallCounter++;
        
        if (Input.GetKeyDown(KeyCode.Minus))
            manager.itemFallCounter--;
    }
}

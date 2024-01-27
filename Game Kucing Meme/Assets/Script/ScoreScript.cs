using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score = 0;
    public TMP_Text Skor;
    public ItemManager manager;

    private void Start()
    {
        Debug.Log("OnCLick akses");
        foreach (var item in manager.items)
        {
            item.onItemClicked += OnItemClicked;
        }
    }

    private void OnItemClicked(Item arg0, GameObject arg1)
    {
        IncreaseScore();
    }

    private void IncreaseScore()
    {
        score++;
        Skor.text = score.ToString();
        Debug.Log("Skor 1");

        if (PlayerPrefs.HasKey("HS"))
        {
            if (score > PlayerPrefs.GetInt("HS"))
            {
                PlayerPrefs.SetInt("HS", score);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TMP_Text highscore;

    private void Awake()
    {
        highscore.text = PlayerPrefs.GetInt("HS").ToString();
    }
}

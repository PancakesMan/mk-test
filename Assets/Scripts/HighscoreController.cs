using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    private Text highscore;

    void Awake()
    {
        highscore = GetComponent<Text>();
        highscore.text = "High Score: " + PlayerPrefs.GetInt("Highscore", 0);
    }
}

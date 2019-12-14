using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    private Text highscore;  // Text displaying the Highscore

    void Awake()
    {
        // Find the Text component
        highscore = GetComponent<Text>();

        // If highscore is not null
        if (highscore)
        {
            // Display the highscore stored in PlayerPrefs
            highscore.text = "High Score: " + PlayerPrefs.GetInt("Highscore", 0);
        }
    }
}

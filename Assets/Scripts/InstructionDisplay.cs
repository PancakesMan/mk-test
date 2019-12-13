using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplay : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void ShowInstructions()
    {
        text.text = "Tap to Jump!";
        Invoke("HideInstructions", 1.5f);
    }

    public void HideInstructions()
    {
        text.text = "Instructions";
    }
}

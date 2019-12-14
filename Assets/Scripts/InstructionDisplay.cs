using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplay : MonoBehaviour
{
    private Text text; // Text component on the instructions button

    private void Awake()
    {
        // Get the Text component
        text = GetComponentInChildren<Text>();
    }

    public void ShowInstructions()
    {
        // Show the instructions
        text.text = "Tap to Jump!";

        // Hide the instructions after 1.5 seconds
        Invoke("HideInstructions", 1.5f);
    }

    public void HideInstructions()
    {
        // Set the button text back to Instructions
        text.text = "Instructions";
    }
}

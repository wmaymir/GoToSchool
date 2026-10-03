using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234"; // The correct code to unlock
    private string enteredCode = ""; // The code entered by the user
    public UnityEvent CorrectCodeEntered; // Event triggered when the correct code is entered

    public void EnterDigit(int digit)
    {
        if (enteredCode.Length < correctCode.Length) // Limit the entered code length to the correct code length
        {
            enteredCode += digit; // Append the digit to the entered code
            Debug.Log("Entered Code: " + enteredCode); // Log the current entered code
        }
    }

    public void CheckCode()
    {
        if (enteredCode == correctCode) // Check if the entered code matches the correct code
        {
            Debug.Log("Correct Code Entered!"); // Log success message
            CorrectCodeEntered.Invoke(); // Trigger the event for correct code
        }
        else
        {
            Debug.Log("Incorrect Code. Try Again."); // Log failure message
            enteredCode = ""; // Reset the entered code for another attempt
        }
    }

    public void BackspaceCode()
    {
        Debug.Log("Backspace Pressed. Removing last digit."); // Log backspace action
        if (enteredCode.Length > 0)
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1); // Remove the last digit
            Debug.Log("Entered Code: " + enteredCode); // Log the current entered code
        }
    }

    public string GetEnteredCode()
    {
        return enteredCode; // Return the current entered code
    }
}

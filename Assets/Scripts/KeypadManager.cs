using System;
using System.Collections;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234"; // The correct code to unlock
    private string enteredCode = ""; // The code entered by the user

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
            // Add logic to unlock or perform an action here
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

    public void InputBuffer(float time)
    {
        StartCoroutine(InputBufferCoroutine(time)); // Start the input buffer coroutine
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    IEnumerable InputBufferCoroutine(float time)
    {
        Debug.Log("Input Buffer Started. Disabling input for " + time + " seconds."); // Log input buffer start
        // Disable input here (e.g., disable button interactions)
        yield return new WaitForSeconds(time); // Wait for the specified time
        Debug.Log("Input Buffer Ended. Re-enabling input."); // Log input buffer end
        // Re-enable input here (e.g., enable button interactions)
    }
}

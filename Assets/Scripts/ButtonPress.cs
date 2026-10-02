using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float pressDistance = 0.02f; // Distance the button moves when pressed
    [SerializeField] private int buttonNumber; // Number of the button for identification

    [Header("Events")]
    public UnityEvent<int> OnPressed; // Event triggered when the button is pressed
    public UnityEvent<int> OnReleased; // Event triggered when the button is released

    private Vector3 initialPosition; // Initial position of the button
    private bool isPressed = false; // State of the button

    void Start()
    {
        initialPosition = transform.localPosition; // Store the initial position of the button

        string buttonName = gameObject.name; // Get the name of the button GameObject
        Match match = Regex.Match(buttonName, @"Button(\d+)"); // Use regex to extract the button number from the name
        if (match.Success)
        {
            buttonNumber = int.Parse(match.Groups[1].Value); // Set the button number to the extracted value
        }
        else
        {
            buttonNumber = 10; // Default to 10 if the name is not a valid number
        }
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(transform.localPosition, initialPosition); // Calculate the current distance from the initial position

        if (currentDistance >= pressDistance && !isPressed) // Check if the button is pressed
        {
            isPressed = true; // Update the state to pressed
            OnPressed.Invoke(buttonNumber); // Trigger the OnPressed event
            Debug.Log("Button " + buttonNumber + " Pressed!"); // Log the button press event
        }
        else if (currentDistance < pressDistance && isPressed) // Check if the button is released
        {
            isPressed = false; // Update the state to released
            OnReleased.Invoke(buttonNumber); // Trigger the OnReleased event
            Debug.Log("Button " + buttonNumber + " Released!"); // Log the button release event
        }
    }

    public int GetButtonNumber()
    {
        return buttonNumber; // Return the button number
    }
}

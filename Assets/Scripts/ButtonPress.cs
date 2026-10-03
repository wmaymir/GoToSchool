using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float pressDistance = 0.25f; // Distance the button moves when pressed
    [SerializeField] private float maxPressDistance = 0.5f; // Maximum distance the button can move
    [SerializeField] private float moveSpeed = 5f; // Speed at which the button moves when pressed
    [SerializeField] private int buttonNumber; // Number of the button for identification

    [Header("Events")]
    public UnityEvent<int> OnPressed; // Event triggered when the button is pressed

    private Vector3 initialPosition; // Initial position of the button
    private Vector3 pressedPosition; // Position of the button when pressed
    private Vector3 maxPressedPosition; // Maximum position of the button when fully pressed
    private bool isPressed = false; // State of the button
    private bool isBeingPressed = false; // State to check if the button is currently being pressed

    void Start()
    {
        initialPosition = transform.localPosition; // Store the initial position of the button
        Debug.Log("Button initial position: " + initialPosition); // Log the initial position for debugging

        pressedPosition = (-transform.forward * pressDistance) + initialPosition; // Calculate the pressed position based on the facing direction and press distance
        Debug.Log("Pressed button position is: " + pressedPosition); // Log the facing direction for debugging)

        maxPressedPosition = (-transform.forward * maxPressDistance) + initialPosition; // Calculate the maximum pressed position based on the facing direction and max press distance
        Debug.Log("Max pressed button position is: " + maxPressedPosition); // Log the facing direction for debugging

        LockRigidBody(GetFacingDirection()); // Lock the Rigidbody constraints based on the facing direction of the button

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
            Debug.Log("Button " + buttonNumber + " Released!"); // Log the button release event
        }

        if ((currentDistance > maxPressDistance) && isBeingPressed) // Check if the button has exceeded the maximum press distance
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, maxPressedPosition, moveSpeed * Time.deltaTime); // Clamp the button position to the maximum pressed position
        }
        else if (!isBeingPressed) // If the button is not being pressed
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, moveSpeed * Time.deltaTime); // Smoothly move the button towards the pressed position
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Keypad")) return; // Ignore collisions with objects tagged as "Keypad"

        isBeingPressed = true; // Set the state to indicate the button is being pressed
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Keypad")) return; // Ignore collisions with objects tagged as "Keypad"

        isBeingPressed = false; // Set the state to indicate the button is no longer being pressed
    }

    public int GetButtonNumber()
    {
        return buttonNumber; // Return the button number
    }

    private char GetFacingDirection()
    {
        Vector3 forward = transform.forward; // Get the forward direction of the button

        if ((forward == new Vector3(1f, 0f, 0f)) || (forward == new Vector3(-1f, 0f, 0f))) return 'X'; // Return 'X' if facing along the X-axis
        if ((forward == new Vector3(0f, 1f, 0f)) || (forward == new Vector3(0f, -1f, 0f))) return 'Y'; // Return 'Y' if facing along the Y-axis
        if ((forward == new Vector3(0f, 0f, 1f)) || (forward == new Vector3(0f, 0f, -1f))) return 'Z'; // Return 'Z' if facing along the Z-axis

        return 'U'; // Return 'U' for undefined direction
    }

    private void LockRigidBody(char axis)
    {
        Rigidbody rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the button
        if (rb != null) // Check if the Rigidbody exists
        {
            switch (axis) // Lock the position and rotation based on the facing direction
            {
                case 'X':
                    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // Lock X position and all rotations
                    break;
                case 'Y':
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // Lock Y position and all rotations
                    break;
                case 'Z':
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; // Lock Z position and all rotations
                    break;
                default:
                    rb.constraints = RigidbodyConstraints.FreezeAll; // Lock all positions and rotations for undefined direction
                    break;
            }
        }
    }
}

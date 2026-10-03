using TMPro;
using UnityEngine;

public class PressurePlateMovement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 pressedPosition;
    public bool IsPressed => isPressed;

    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 0.1f; // Distance to move down when pressed
    [SerializeField] private float moveSpeed = 2f; // Speed of movement
    [SerializeField] private bool isPressed = false;

    [Header("Tag Settings")]
    [SerializeField] private string activateTag;

    void Start()
    {
        initialPosition = transform.position;
        float pressedY = initialPosition.y - moveDistance;
        pressedPosition = new Vector3(initialPosition.x, pressedY, initialPosition.z);
    }

    void FixedUpdate()
    {
        if (isPressed)
        {
            // Move down when pressed
            transform.position = Vector3.Lerp(transform.position, pressedPosition, Time.fixedDeltaTime * moveSpeed);
        }
        else if (!isPressed)
        {
            // Move back to initial position when not pressed
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.fixedDeltaTime * moveSpeed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(activateTag))
        {
            isPressed = true;
            Debug.Log("Pressure plate activated by: " + other.collider.name);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag(activateTag))
        {
            isPressed = false;
            Debug.Log("Pressure plate deactivated by: " + other.collider.name);
        }
    }
}

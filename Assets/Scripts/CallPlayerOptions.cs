using UnityEngine;
using UnityEngine.InputSystem;

public class CallPlayerOptions : MonoBehaviour
{

    //Google Gemini helped Westin in writing part of this script

    private bool optionsOn = false;

    [SerializeField]
    public GameObject PlayerOptionsCanvas;

    [SerializeField] private InputActionReference customActionReference;

    private void OnEnable()
    {
        customActionReference.action.performed += OnButtonPressed;
    }

    private void OnDisable()
    {
        customActionReference.action.performed -= OnButtonPressed;
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        ToggleOptionsScreen();
    }

    private void ToggleOptionsScreen()
    {
        if (optionsOn)
        {
            Time.timeScale = 1;
            PlayerOptionsCanvas.SetActive(false);
            optionsOn = false;
        }
        else
        {
            Time.timeScale = 0;
            PlayerOptionsCanvas.SetActive(true);
            optionsOn = true;
        }
    }
}
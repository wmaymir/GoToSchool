using UnityEngine;
using UnityEngine.InputSystem;

public class CallPlayerOptions : MonoBehaviour
{

    private bool optionsOn = false;

    [SerializeField]
    public GameObject PlayerOptionsCanvas;

    [SerializeField] private InputActionReference menuActionReference;

    private void OnEnable()
    {
        menuActionReference.action.performed += OnButtonPressed;
    }

    private void OnDisable()
    {
        menuActionReference.action.performed -= OnButtonPressed;
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
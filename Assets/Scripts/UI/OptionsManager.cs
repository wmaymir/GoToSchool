using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Google Gemini helped Westin in writing part of this script

public class CallPlayerOptions : MonoBehaviour
{
    [SerializeField]
    public GameObject PlayerOptionsCanvas;
    public InputActionReference customActionReference; // search "menu" in inspector if lost (left menu toggle)
    public AudioListener Listener;

    //all scipts can get this information for pausing, but not change it
    public bool optionsOn { get; private set; } = false;
    bool muted = false;

    private void Awake()
    {
        //makes sure scene doesn't load paused if accesed via the options screen
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    private void OnEnable()
    {
        //enable the action
        customActionReference.action.Enable();

        //adds OnButtonPressed to Unity's built-in event for when an action is performed
        //+= links the function to the event
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

    public void OnMuteButtonClicked()
    {
        //Debug.Log("Mute button was clicked.");
        if (!muted)
        {
            muted = true;
            Listener.enabled = false;
        }
        else
        {
            muted = false;
            Listener.enabled = true;
        }
    }

    public void OnTitleButtonClicked()
    {
        //Debug.Log("Title button was clicked.");
        SceneManager.LoadSceneAsync("Title");
    }
}
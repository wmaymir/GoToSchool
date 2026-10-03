using System.Threading;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{

    [SerializeField]
    public GameObject LoadingText;
    public AudioListener Listener;

    bool muted = false;

    private void Start()
    {
        //makes sure scene doesn't load paused if accesed via the options screen
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play button was clicked.");
        LoadingText.SetActive(true);
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button was clicked.");
        Application.Quit();
    }

    public void OnMuteButtonClicked()
    {
        Debug.Log("Mute button was clicked.");
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
        Debug.Log("Title button was clicked.");
        SceneManager.LoadSceneAsync("Title");
    }

}

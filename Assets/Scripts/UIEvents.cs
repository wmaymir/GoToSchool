using System.Threading;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{

    [SerializeField]
    private GameObject LoadingText;
    private AudioListener Listener;

    bool muted = false;

    public void OnPlayButtonClicked()
    {
        //Debug.Log("Play button was clicked.");
        LoadingText.SetActive(true);
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OnQuitButtonClicked()
    {
        //Debug.Log("Quit button was clicked.");
        Application.Quit();
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

using System.Threading;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    [SerializeField]
    public GameObject LoadingText;

    private void Awake()
    {
        //makes sure scene doesn't load paused if accesed via the options screen
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    public void OnPlayButtonClicked()
    {
        //Debug.Log("Play button was clicked.");
        LoadingText.SetActive(true);
        SceneManager.LoadSceneAsync("EnvironmentScene");
    }

    public void OnQuitButtonClicked()
    {
        //Debug.Log("Quit button was clicked.");
        Application.Quit();
    }

}

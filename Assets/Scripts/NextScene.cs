using UnityEngine;

public class NextScene : MonoBehaviour
{
    public GameObject gameManager;
    public GameManager gameManagerScript;

    private void Awake()
    {

        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Bork");
        if (collision.gameObject.tag == "Player")
        {
            gameManagerScript.LoadNextScene();
            Debug.Log("MEOW");
        }
    }
}

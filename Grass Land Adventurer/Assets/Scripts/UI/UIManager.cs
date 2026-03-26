using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject gameOver;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
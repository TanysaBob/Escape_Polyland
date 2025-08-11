using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume time
        isPaused = false;

        // Optionally reset other states if needed
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f; // Unpause before reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }

}

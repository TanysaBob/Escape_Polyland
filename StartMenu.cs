using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene (index 1)
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
    
}

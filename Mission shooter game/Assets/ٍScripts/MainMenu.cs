using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("MissionShooter");
    }
    public void OnLoadMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnExitButton()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }
}

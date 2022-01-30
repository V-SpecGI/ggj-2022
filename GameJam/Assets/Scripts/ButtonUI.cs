using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string GameLevel = "";
    [SerializeField] private string SettingsMenu = "";
    [SerializeField] private string previousScreen = "";
    private string currentScreen = "Main Menu";

    public void PlayButton()
    {
        currentScreen = GameLevel;
        SceneManager.LoadScene(GameLevel);
    }

    public void SettingsButton()
    {
        currentScreen = SettingsMenu;
        SceneManager.LoadScene(SettingsMenu);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SceneManager.LoadScene(previousScreen);
    }
}

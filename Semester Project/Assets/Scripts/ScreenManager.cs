using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    // go to title screen
    public void TitleScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    // start game
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    // go to credit screen
    public void CreditScreen()
    {
        SceneManager.LoadScene("CreditScreen");
    }

    // go to controls screen
    public void ControlsScreen()
    {
        SceneManager.LoadScene("ControlsScreen");
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}

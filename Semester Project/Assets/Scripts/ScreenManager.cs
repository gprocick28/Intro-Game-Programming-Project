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
        SceneManager.LoadScene("LevelSelectScreen");
    }

    // go to credit screen
    public void CreditScreen()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    // go to controls screen
    public void ControlsScreen()
    {
        SceneManager.LoadScene("HowToPlayScreen");
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // go to level one
    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOneScreen");
    }

    // go to level two
    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwoScreen");
    }

    // go to level three
    public void LevelThree()
    {
        SceneManager.LoadScene("LevelThreeScreen");
    }

    // go to level four
    public void LevelFour()
    {
        SceneManager.LoadScene("LevelFourScreen");
    }

    // go to level five
    public void LevelFive()
    {
        SceneManager.LoadScene("LevelFiveScreen");
    }
}

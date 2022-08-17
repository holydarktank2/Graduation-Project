using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void TurningLeft()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void loseplayagain()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedOverScene"));
    }

    public void completeplayagain()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedFinishScene"));
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedFinishScene")+1);
    }

    public void lastlevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedFinishScene") -1);
    }

    public void quitgame()
    {
        
        SceneManager.LoadScene(0);
        
    }
    
    public void LevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void TurningRight()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void standard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void taiwan()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }

    public void merge()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }

    public void equal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(7);
    }

    public void priority()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(8);
    }

    public void chaocher()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(9);
    }
}

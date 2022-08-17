using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public static bool isGamePaused = false;
    public static List<int> Leftover = new List<int>();

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject Playing;

    void Start()
    {
        Leftover.Clear();
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    void Update()
    {
        Leftover = player.passedcheckpoint;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Playing.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    public void PauseGame()
    {
        Playing.SetActive(false);
        pauseMenu.SetActive(true);
        //Debug.Log("¶Â¤Ä¤Ä" + Leftover.Count);
        Time.timeScale = 0f;
        isGamePaused = true;
        for(int i=0;i<Leftover.Count;i++)
        {  
            GameObject mission = GameObject.Find("¶Â¤Ä¤Ä" + Leftover[i].ToString());          
            mission.SetActive(false);
        }
        
    }

    public void ReloadScene()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

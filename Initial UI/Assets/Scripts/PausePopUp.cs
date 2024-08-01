using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopUp : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        pauseMenu.SetActive(true);
        Time.timeScale=0;
    }
    public void HomeButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
    public void RetryButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
    public void ResumeButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    /* public void OnApplicationPause(bool pause)
     {
         if (pause)
         {
             // Application is paused
             Debug.Log("Application paused");
             SaveGameData();
             PauseGameLogic();
         }
         else
         {
             // Application is resumed
             Debug.Log("Application resumed");
             LoadGameData();
             ResumeGameLogic();
         }
     }*/


}

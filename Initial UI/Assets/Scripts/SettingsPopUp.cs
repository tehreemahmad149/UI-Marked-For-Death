using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SettingsPopUp : MonoBehaviour
{
    public static SettingsPopUp Instance;
    [SerializeField] GameObject settingsMenu;

   
   
    public void ResumeButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        settingsMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void SettingsButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        settingsMenu.SetActive(true);
        Time.timeScale = 0;
    }

   
}


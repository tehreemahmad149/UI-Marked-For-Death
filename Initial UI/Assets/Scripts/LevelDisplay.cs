using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public TMPro.TMP_Text levelText; // Reference to the UI Text component

    void Start()
    {
        //int currentLevelIndex = SceneManager.GetActiveScene().buildIndex + 1; // Build index starts from 0, so add 1
        int currentLevelIndex = 0;
        levelText.text = "LEVEL: " + currentLevelIndex;
    }
}

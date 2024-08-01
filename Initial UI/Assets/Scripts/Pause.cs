using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{public static bool isPaused = false;

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
/* for the pausing of other elements
 void Update()
{
    if (!PauseManager.isPaused)
    {
        // Game logic here
    }
}

 
 
 */

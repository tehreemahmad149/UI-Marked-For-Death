using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void GoToHome()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadSceneAsync(0);
    }
}

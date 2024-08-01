using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StaminaManager : MonoBehaviour
{
    public Image staminaBar;
    public float staminaAmount = 100f;

    public GameObject gameOverPanel; // Reference to the Game Over panel
    // Start is called before the first frame update
    void Start()
    {
        /*if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaAmount <= 0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);
        }
    }
    public void TakeDamage(float damage)
    {
        staminaAmount -= damage;
        staminaBar.fillAmount = staminaAmount / 100f;
    }
    public void Heal(float amount)
    {
        staminaAmount += amount;
        staminaAmount = Mathf.Clamp(staminaAmount, 0, 100);
        staminaBar.fillAmount = staminaAmount / 100f;
    }
}

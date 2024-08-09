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

    private GameObject player;
    private CharacterStats characterStats;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                characterStats = player.GetComponent<CharacterStats>();
            }
            else
            {
                Debug.Log("Looking for player...");
                return; // Exit Update if player is not detected
            }
        }

        if (characterStats != null)
        {
            float maxStamina = characterStats.maxStamina;
            float currentStamina = characterStats.currentStamina;
            staminaAmount = (currentStamina / maxStamina) * 100f;
            staminaBar.fillAmount = staminaAmount / 100f;
        }

        if (staminaAmount <= 0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}

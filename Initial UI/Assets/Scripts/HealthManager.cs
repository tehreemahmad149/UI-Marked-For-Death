using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
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
            float maxHealth = characterStats.maxHealth;
            float currentHealth = characterStats.currentHealth;
            healthAmount = (currentHealth / maxHealth) * 100f;
            healthBar.fillAmount = healthAmount / 100f;
        }

        if (healthAmount <= 0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}

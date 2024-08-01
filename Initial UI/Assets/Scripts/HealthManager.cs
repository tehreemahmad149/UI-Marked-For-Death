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
    // Start is called before the first frame update
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
     if (healthAmount<=0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }  
     if (Input.GetKeyDown(KeyCode.Return)) {//dummy call will be replaced with when some attack is detected
            TakeDamage(20);//dummy value
        }
     if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);
        }
    }
 
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount= healthAmount/100f;
    }
    public void Heal(float amount)
    {
        healthAmount += amount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

}

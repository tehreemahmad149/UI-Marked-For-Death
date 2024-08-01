using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpecialManager : MonoBehaviour
{
    public Image specialBar;
    public float specialAmount = 100f;

    public GameObject gameOverPanel; // Reference to the Game Over panel
    // Start is called before the first frame update
    void Start()
    {
        /*if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (specialAmount <= 0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);//20 is dummy
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(20);//20 is dummy
        }
    }
    public void TakeDamage(float damage)
    {
        specialAmount -= damage;
        specialBar.fillAmount = specialAmount / 100f;
    }
    public void Heal(float amount)
    {
        specialAmount += amount;
        specialAmount = Mathf.Clamp(specialAmount, 0, 100);
        specialBar.fillAmount = specialAmount / 100f;
    }
}

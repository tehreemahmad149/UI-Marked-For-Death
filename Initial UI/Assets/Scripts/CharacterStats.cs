using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float strength = 100;
    public float speed = 100;
    public float maxHealth = 100;
    public float maxStamina = 100;
    public float armor = 100;

    public float currentHealth;
    public float currentStamina;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }
    void FixedUpdate()
    {
        RestoreStamina((float)0.2);
    } 

    public void TakeDamage(float amount)
    {
        currentHealth -= amount * (1 - armor / 100); // Simple armor calculation
        // Handle death if health reaches 0
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void ConsumeStamina(float amount)
    {
        currentStamina -= amount;
        // Handle stamina depletion (e.g., slow down character)
    }

    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
    }
}

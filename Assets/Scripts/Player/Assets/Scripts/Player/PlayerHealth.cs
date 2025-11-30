using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDeath;
    
    private bool isDead = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(1f);
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        onHealthChanged?.Invoke(currentHealth / maxHealth);
        
        if (currentHealth <= 0)
            Die();
    }
    
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthChanged?.Invoke(currentHealth / maxHealth);
    }
    
    void Die()
    {
        isDead = true;
        onDeath?.Invoke();
    }
    
    public void Respawn()
    {
        isDead = false;
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(1f);
    }
}

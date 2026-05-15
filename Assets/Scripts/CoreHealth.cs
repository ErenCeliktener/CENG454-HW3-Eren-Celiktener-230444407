using UnityEngine;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("Core Health: " + currentHealth + "/" + maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Core Health: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Core destroyed. Game over.");
        }
    }
}
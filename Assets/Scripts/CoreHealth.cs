using System;
using UnityEngine;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnCoreDestroyed;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("Core Health: " + currentHealth + "/" + maxHealth);
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0)
        {
            return;
        }

        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Core Health: " + currentHealth + "/" + maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Core destroyed. Game over.");
            OnCoreDestroyed?.Invoke();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [Header("Observed Systems")]
    [SerializeField] private CoreHealth coreHealth;

    [Header("Core Health Bar")]
    [SerializeField] private Image coreHealthFillImage;

    private void OnEnable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnHealthChanged += UpdateCoreHealthBar;
        }
    }

    private void OnDisable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnHealthChanged -= UpdateCoreHealthBar;
        }
    }

    private void Start()
    {
        if (coreHealth != null)
        {
            UpdateCoreHealthBar(coreHealth.CurrentHealth, coreHealth.MaxHealth);
        }
    }

    private void UpdateCoreHealthBar(int currentHealth, int maxHealth)
    {
        if (coreHealthFillImage == null || maxHealth <= 0)
        {
            return;
        }

        coreHealthFillImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
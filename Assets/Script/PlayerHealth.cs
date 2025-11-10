using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image HealthBar; 

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0f)
            Die();
    }

    private void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            HealthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} est mort !");
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20f);
            Destroy(collision.gameObject);
        }
    }
}
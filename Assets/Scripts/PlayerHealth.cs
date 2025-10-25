using System;
using UnityEngine;

[RequireComponent(typeof(PlayerClass))]
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float currentHealth { get; private set; }

    private ClassStatsBase stats;
    public float MaxHealth => stats != null ? stats.baseMaxHealth : 100f;

    [Header("Respawn Settings")]
    [Tooltip("Where the player will respawn after death.")]
    public Transform respawnPoint; // Assign this in the Inspector (center of arena)
    private Vector3 initialPosition; // Fallback if no respawnPoint is assigned

    public event Action<float, float> OnHealthChanged;

    void Awake()
    {
        PlayerClass playerClass = GetComponent<PlayerClass>();
        if (playerClass != null && playerClass.stats != null)
        {
            stats = playerClass.stats;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} is missing a PlayerClass or assigned stats. Using default values.");
        }

        initialPosition = transform.position; // Save starting point as backup
    }

    void Start()
    {
        currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

        Debug.Log($"{gameObject.name} took {damage} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        Debug.Log($"{gameObject.name} healed {amount}. Current Health: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        DeathManager deathManager = FindFirstObjectByType<DeathManager>();
        if (deathManager != null)
        {
            deathManager.TriggerDeath();
        }
        else
        {
            Debug.LogWarning("No DeathManager found in the scene!");
        }
    }

    public void Respawn()
    {
        // Reset position
        Vector3 respawnPos = respawnPoint != null ? respawnPoint.position : initialPosition;
        transform.position = respawnPos;

        // Reset health
        currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        Debug.Log($"{gameObject.name} respawned with {currentHealth} health at {respawnPos}.");

        // Reset enemies if a manager exists
        EnemySpawner enemyManager = FindFirstObjectByType<EnemySpawner>();
        if (enemyManager != null)
        {
            enemyManager.ResetEnemies();
            Debug.Log("Enemies reset after respawn.");
        }
        else
        {
            Debug.LogWarning("No EnemyManager found to reset enemies.");
        }
    }
}

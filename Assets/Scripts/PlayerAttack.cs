using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Class Stats")]
    public ClassStatsBase stats;  // Assign the ScriptableObject (MageStats, PaladinStats, etc.) here

    private float fireTimer = 0f;

    void Start()
    {
        // Optional safety check
        if (stats == null)
        {
            Debug.LogWarning("ClassStats not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        if (stats == null) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Enemy nearest = FindNearestEnemy();
            if (nearest != null)
            {
                FireAt(nearest.transform.position);
                // Use baseFireRate from ScriptableObject
                fireTimer = 1f / stats.baseFireRate;
            }
        }
    }

    void FireAt(Vector3 targetPos)
    {
        // Spawn projectile at fire point
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector3 dir = (targetPos - firePoint.position).normalized;

        Projectile projectileScript = proj.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(dir);

            // Damage is scaled by the class's core stat
            projectileScript.damage = stats.baseProjectileDamage * stats.GetCoreStat();

            // Optional: If you want a separate stat multiplier for abilities
            // projectileScript.statMultiplier = stats.GetCoreStat();
        }
    }

    Enemy FindNearestEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Enemy nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Enemy e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = e;
            }
        }

        return nearest;
    }
}

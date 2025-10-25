using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public float damage = 20f;

    // Optional: multiplier from class stats
    [HideInInspector] public float statMultiplier = 1f;

    private Vector3 targetDirection;

    public void SetDirection(Vector3 dir)
    {
        targetDirection = dir.normalized;
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage * statMultiplier); // scale by class stat
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Arena"))
        {
            Destroy(gameObject);
        }
    }
}

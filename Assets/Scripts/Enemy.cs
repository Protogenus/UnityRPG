using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 50f;
    public float damage = 10f;
    public float moveSpeed = 5f;
    public float contactDamageRate = 0.5f; // seconds between damage ticks

    private Transform player;
    private Rigidbody rb;
    private float lastDamageTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // keep enemy upright

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Move towards player using Rigidbody
        Vector3 direction = (player.position - rb.position).normalized;
        direction.y = 0; // keep grounded
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Rotate to face player
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(lookDir);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= contactDamageRate)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}

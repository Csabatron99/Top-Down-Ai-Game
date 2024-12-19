using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lifetime = 5f; // Time before projectile auto-destroys

    private void Start()
    {
        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for objects with a Health component
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(50); // Adjust damage amount as needed
            Debug.Log($"{collision.gameObject.name} hit by projectile!");
            Destroy(gameObject); // Destroy the projectile
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the projectile when hitting an obstacle
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab; // Prefab of the projectile
    public Transform firePoint;         // The point from where the projectile will spawn
    public float projectileSpeed = 20f; // Speed of the projectile
    public float fireRate = 0.5f;       // Time between shots
    public Camera mainCamera;           // Reference to the main camera

    private float nextFireTime = 0f;    // Timer to manage fire rate

    void Update()
    {
        RotateTowardsMouse();
        HandleShooting();
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set Z to 0 since we're in 2D or top-down view

        // Calculate the direction to the mouse
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Rotate the firePoint to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Apply velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * projectileSpeed; // firePoint.right is now aligned with the mouse
        }
    }
}

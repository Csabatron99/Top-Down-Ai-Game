using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Header("Sight Settings")]
    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private float sightAngle = 45f;
    [SerializeField] private LayerMask targetLayer;  // Assign a layer for enemies or players

    public bool IsTargetInSight(Transform target)
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > sightDistance)
            return false;

        float angle = Vector2.Angle(transform.up, directionToTarget);
        if (angle > sightAngle)
            return false;

        // Perform a raycast to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, sightDistance, targetLayer);
        return hit.collider != null && hit.collider.transform == target;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the sight cone for visualization
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -sightAngle) * transform.up * sightDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, sightAngle) * transform.up * sightDistance;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
}
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;


public class EnemyAgent : Agent
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform torso;

    [Header("Sight System")]
    [SerializeField] private EnemySight sightSystem;
    [SerializeField] private Transform targetEnemy;  // Reference to the opponent
    private Vector2 movementDirection;
    private Rigidbody2D rb;

    private Health healthComponent;
    private int previousHealth;
    public override void Initialize()
    {
        healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            previousHealth = healthComponent.CurrentHealth;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset positions of both agents
        transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        targetEnemy.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add observations: relative position to the target, self-health, etc.
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetEnemy.position);
        sensor.AddObservation(sightSystem.IsTargetInSight(targetEnemy));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Decode actions
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        bool shoot = actions.DiscreteActions[0] == 1;

        // Apply movement
        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        // Shooting behavior
        if (shoot && sightSystem.IsTargetInSight(targetEnemy))
        {
            Shoot();
            AddReward(1.0f); // Reward for shooting at a visible target
        }
        else if (shoot)
        {
            AddReward(-0.1f); // Penalty for shooting without seeing a target
        }

        // Penalty for taking damage (add this in a health script or event-based system)
        if (healthComponent != null && healthComponent.CurrentHealth < previousHealth)
        {
            AddReward(-1.0f); // Penalize for taking damage
            previousHealth = healthComponent.CurrentHealth;
        }

        // Small reward for survival
        AddReward(0.01f * Time.deltaTime);
    }   

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // For testing without training, you can control the agent manually
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");

        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

     private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, weapon.position, weapon.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = weapon.right * 10f;
    }

    public void RewardAction(float reward)
    {
        AddReward(reward);
    }
}

using System;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public bool isMoving = false; // Flag to check if the train is moving
    private bool onPath1 = true; // Track whether the train is on path 1 or path 2

    public event Action OnPathComplete; // Event to notify that the path is complete

    public Transform[] path1Waypoints; // Waypoints for path 1
    public Transform[] path2Waypoints; // Waypoints for path 2

    private int currentWaypointIndex = 0; // Index to track the current waypoint the train is heading towards
    private float moveSpeed = 5f; // Movement speed of the train

    private Vector3 targetPosition; // The target position the train is moving towards
    private bool isMovingToTarget = false; // Flag to check if the train is moving towards the target position

    void Start()
    {
        // Initialize the train position based on the starting path
        if (path1Waypoints.Length > 0)
        {
            // Start the train at the first waypoint of Path 1
            transform.position = path1Waypoints[0].position;
            targetPosition = path1Waypoints[0].position;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the train's trigger collides with a person
        if (other.CompareTag("Person"))
        {
            // Destroy the person when the train passes over them
            Destroy(other.gameObject);
            Debug.Log("Person destroyed by trigger: " + other.gameObject.name);
        }
    }
    void Update()
    {
        // If the train is allowed to move, handle movement towards the target
        if (isMoving && isMovingToTarget)
        {
            MoveTrain();
        }
    }

    // Switch the train to Path 1
    public void SwitchToPath1()
    {
        onPath1 = true;
        currentWaypointIndex = 0; // Start from the first waypoint on Path 1
        targetPosition = path1Waypoints[currentWaypointIndex].position;
        isMovingToTarget = true; // Enable movement towards the first waypoint on Path 1
        Debug.Log("Switched to Path 1");
    }

    // Switch the train to Path 2
    public void SwitchToPath2()
    {
        onPath1 = false;
        currentWaypointIndex = 0; // Start from the first waypoint on Path 2
        targetPosition = path2Waypoints[currentWaypointIndex].position;
        isMovingToTarget = true; // Enable movement towards the first waypoint on Path 2
        Debug.Log("Switched to Path 2");
    }

    // Allow the train to start moving
    public void AllowMovement()
    {
        isMoving = true;
        Debug.Log("Train is moving...");
    }

    // Stop the train's movement
    public void StopMovement()
    {
        isMoving = false;
        isMovingToTarget = false;
        Debug.Log("Train stopped.");
    }

    // Handles the movement logic
    private void MoveTrain()
    {
        // Move the train towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the train reaches the target position, update to the next waypoint
        if (transform.position == targetPosition)
        {
            currentWaypointIndex++;

            // Check if we've reached the last waypoint
            if (onPath1 && currentWaypointIndex < path1Waypoints.Length)
            {
                targetPosition = path1Waypoints[currentWaypointIndex].position;
            }
            else if (!onPath1 && currentWaypointIndex < path2Waypoints.Length)
            {
                targetPosition = path2Waypoints[currentWaypointIndex].position;
            }
            else
            {
                // Path complete, trigger the event to notify that the path is done
                OnPathComplete?.Invoke();
                StopMovement(); // Stop the train once the path is complete
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            Destroy(other.gameObject); // Remove the person from the scene
            Debug.Log("Person hit and removed!");
        }
    }
}

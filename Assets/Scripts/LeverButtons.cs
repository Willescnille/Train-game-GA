using UnityEngine;
using UnityEngine.UI;

public class LeverButtons : MonoBehaviour
{
    public PathFollower pathFollower; // Reference to PathFollower script
    public Button pullButton; // Button for pulling the lever (switch tracks)
    public Button dontPullButton; // Button for not pulling the lever (stay on current track)

    private bool waitingForInput = false; // Track if we're waiting for player input

    void Start()
    {
        // Set up button listeners
        pullButton.onClick.AddListener(OnPullButtonPressed);
        dontPullButton.onClick.AddListener(OnDontPullButtonPressed);

        // Initially, buttons should be hidden
        pullButton.gameObject.SetActive(false);
        dontPullButton.gameObject.SetActive(false);
    }

    public void StartScenario()
    {
        // Display the buttons and wait for player input
        waitingForInput = true;
        pullButton.gameObject.SetActive(true);
        dontPullButton.gameObject.SetActive(true);

        // Stop the movement until player makes a decision
        pathFollower.StopMovement();
    }

    void OnPullButtonPressed()
    {
        if (!waitingForInput) return;

        // Switch to path 2 (if the player wants to switch the track)
        pathFollower.SwitchToPath2();

        // Allow the train to start moving
        pathFollower.AllowMovement();

        // Hide the buttons after making a decision
        pullButton.gameObject.SetActive(false);
        dontPullButton.gameObject.SetActive(false);

        waitingForInput = false;
    }

    void OnDontPullButtonPressed()
    {
        if (!waitingForInput) return;

        // Keep the train on path 1 (if the player doesn't want to switch the track)
        pathFollower.SwitchToPath1();

        // Allow the train to start moving
        pathFollower.AllowMovement();

        // Hide the buttons after making a decision
        pullButton.gameObject.SetActive(false);
        dontPullButton.gameObject.SetActive(false);

        waitingForInput = false;
    }
}

using UnityEngine;
using UnityEngine.UI; // Required for UI components


[System.Serializable]



public class Scenario
{

    public string description;  // Add description field to Scenario class
    public GameObject[] path1PeoplePrefabs;
    public GameObject[] path2PeoplePrefabs;
    public Transform[] path1PeoplePositions;
    public Transform[] path2PeoplePositions;
}





public class ScenarioManager : MonoBehaviour
{

    public ScenarioTextManager scenarioTextManager;
    public PathFollower pathFollower; // Reference to PathFollower
    public PeopleContainer peopleContainer; // Reference to PeopleContainer
    public Scenario[] scenarios; // Array of all scenarios
    private int currentScenarioIndex = 0; // Track the current scenario

    public Button pullButton; // Button for pulling the lever (switch tracks)
    public Button dontPullButton; // Button for not pulling the lever (stay on current track)

    private bool waitingForInput = false; // Track if we're waiting for player input
    public Transform trainStartPosition; // Reference to the train's starting position
    
    void Start()
    {
        if (pathFollower != null)
        {
            pathFollower.OnPathComplete += HandlePathComplete; // Subscribe to path completion event
        }
        else
        {
            Debug.LogError("PathFollower is not assigned in ScenarioManager.");
        }

        // Set up the buttons
        pullButton.onClick.AddListener(OnPullButtonPressed); // Action for Pull button
        dontPullButton.onClick.AddListener(OnDontPullButtonPressed); // Action for Don't Pull button

        // Load the first scenario
        LoadScenario(currentScenarioIndex);
    }

    void LoadScenario(int index)
    {
        currentScenarioIndex = index;
    
        // Check if we are out of bounds
        if (index >= scenarios.Length)
        {
            Debug.Log("No more scenarios to load. Game over!");
            return;
        }
        // Reset the train's position to the starting position
        if (trainStartPosition != null)
        {
            pathFollower.transform.position = trainStartPosition.position;
            pathFollower.transform.rotation = trainStartPosition.rotation; // Ensure the rotation is also reset
        }
        else
        {
            Debug.LogError("Train Start Position is not assigned in ScenarioManager.");
        }
        {
            scenarioTextManager.ShowText(index);
        }


        // Load the scenario data
        Scenario selectedScenario = scenarios[currentScenarioIndex];
        Debug.Log("Loading scenario: " + selectedScenario.description);

        // Set up the people on the tracks using the PeopleContainer
        peopleContainer.SetupPeople(selectedScenario);

        // Wait for the player input before moving the trolley
        waitingForInput = true;

        // Show the buttons for decision
        pullButton.gameObject.SetActive(true);
        dontPullButton.gameObject.SetActive(true);
       
    


        // Stop the movement until the player makes a choice
        pathFollower.StopMovement();
    }

    // Called when the "Pull" button is pressed
    void OnPullButtonPressed()
    {
        if (!waitingForInput)
            return;

        // Switch to path 2 (if the player wants to switch the track)
        pathFollower.SwitchToPath2();

        // Allow the train to start moving
        pathFollower.AllowMovement();

        // Disable the buttons and start the train's movement
        pullButton.gameObject.SetActive(false);
        dontPullButton.gameObject.SetActive(false);

        waitingForInput = false;
    }

    // Called when the "Don't Pull" button is pressed
    void OnDontPullButtonPressed()
    {
        if (!waitingForInput)
            return;

        // Keep the trolley on path 1
        pathFollower.SwitchToPath1();

        // Allow the train to start moving
        pathFollower.AllowMovement();

        // Disable the buttons and start the train's movement
        pullButton.gameObject.SetActive(false);
        dontPullButton.gameObject.SetActive(false);

        waitingForInput = false;
    }

    void HandlePathComplete()
    {
        // Move to the next scenario when the current path is completed
        currentScenarioIndex++;
        LoadScenario(currentScenarioIndex); // Load the next scenario
    }

    void OnDestroy()
    {
        // Unsubscribe from the path complete event when this script is destroyed
        if (pathFollower != null)
        {
            pathFollower.OnPathComplete -= HandlePathComplete;
        }

        // Unsubscribe from the button events
        pullButton.onClick.RemoveListener(OnPullButtonPressed);
        dontPullButton.onClick.RemoveListener(OnDontPullButtonPressed);
    }
    
}



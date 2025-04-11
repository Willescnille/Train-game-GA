using UnityEngine;

public class PeopleContainer : MonoBehaviour
{
    // This method will set up the people based on the scenario passed in
    public void SetupPeople(Scenario scenario)
    {
        // Clear any existing people
        ClearExistingPeople();

        // Set up people for path 1
        for (int i = 0; i < scenario.path1PeoplePrefabs.Length; i++)
        {
            if (i < scenario.path1PeoplePositions.Length)
            {
                // Instantiate people at their respective positions on Path 1
                Instantiate(scenario.path1PeoplePrefabs[i], scenario.path1PeoplePositions[i].position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Not enough positions for people on Path 1.");
            }
        }

        // Set up people for path 2
        for (int i = 0; i < scenario.path2PeoplePrefabs.Length; i++)
        {
            if (i < scenario.path2PeoplePositions.Length)
            {
                // Instantiate people at their respective positions on Path 2
                Instantiate(scenario.path2PeoplePrefabs[i], scenario.path2PeoplePositions[i].position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Not enough positions for people on Path 2.");
            }
        }
    }

    private void ClearExistingPeople()
    {
        // Destroy all previously instantiated people in the container
        foreach (GameObject person in GameObject.FindGameObjectsWithTag("Person"))
        {
            Destroy(person);
        }
    }
}

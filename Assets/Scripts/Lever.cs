using UnityEngine;

public class Lever : MonoBehaviour
{
    public PathFollower trolleyPathFollower; // Reference to the trolley's PathFollower script

    void OnMouseDown()
    {
        // Switch to Path 2 when the lever is pulled
        if (trolleyPathFollower != null)
        {
            trolleyPathFollower.SwitchToPath2();
        }
    }
}

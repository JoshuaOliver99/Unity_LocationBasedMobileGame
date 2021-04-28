using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    // GameObjects to be enabled when LocationServices are connected
    [SerializeField] List<GameObject> LocationServiceDependent;

    // Used in Loading scene
    [SerializeField] private Text StatusText;

    // Flags
    [SerializeField] private bool Connected = false;
    // NOTE: Seemingly unneccessary     [SerializeField] private bool Connecting = false; 

    private void Update()
    {
        // Test location service status
        QueryLocationStatus();
    }

    private void QueryLocationStatus()
    {
        // If (Location service is connected)...
        if (Input.location.status == LocationServiceStatus.Running && !Connected)
        {
            // Update status message
            StatusText.text = ("LocationService: Running!"); // DEBUG and can be removed

            // Enable all dependent GameObjects
            foreach (GameObject gameObject in LocationServiceDependent)
                gameObject.SetActive(true);

            Connected = true; // Set state flag
        }
        // If (location service is not connected & not connecting)...
        else if (Input.location.status != LocationServiceStatus.Running && !LocationServiceManager.connecting )// !Connecting)
        {
            // Update status message
            StatusText.text = ("LocationService: Not running! - Connecting");

            // Try connect to location services...
            StartCoroutine(LocationServiceManager.ConnectLocationService());

            // Set state flag
            Connected = false;
        }
        // If (Location service connection failed)...
        else if (Input.location.status == LocationServiceStatus.Failed)
        {
            // Update status message
            StatusText.text = ("LocationService: Failed!");

            // set state flag
            Connected = false;
        }
    }

}
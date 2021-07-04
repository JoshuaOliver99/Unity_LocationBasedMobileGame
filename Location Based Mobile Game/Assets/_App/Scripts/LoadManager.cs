using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    /// <summary> 
    /// GameObjects to be enabled once LocationServices are connected
    /// </summary>
    [SerializeField] private List<GameObject> locationServiceDependent;

    private bool connected = false;

    private void Awake()
    {
        ManageIncorrectScene();
    }

    private void Update()
    {
        ManageLocationStatus();
    }

    /// <summary>
    /// Test location service status, and act appropriately
    /// </summary>
    private void ManageLocationStatus()
    {
        // If (Location service is running)...
        if (Input.location.status == LocationServiceStatus.Running && !connected)
        {
            // Enable all dependent GameObjects
            foreach (GameObject gameObject in locationServiceDependent)
                gameObject.SetActive(true);

            connected = true;
        }
        // If (location service is not running & not connecting)...
        else if (Input.location.status != LocationServiceStatus.Running && !LocationServiceManager.Connecting )
        {
            // Try connect to location services
            StartCoroutine(LocationServiceManager.ConnectLocationService());

            connected = false;
        }
        // If (Location service connection failed)...
        else if (Input.location.status == LocationServiceStatus.Failed)
        {
            connected = false;
        }
    }

    /// <summary>
    /// Disables this component if on the incorrect scene
    /// </summary>
    private void ManageIncorrectScene()
    {   
        // Get the active scene
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        // IF (Map, Interaction or Loading scene is not active)...
        if (scene.name == "Map" || scene.name == "Interaction" || scene.name == "Loading")
        { }
        else
            gameObject.GetComponent<LoadManager>().enabled = false; // Disable this gameobject

    }

}
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

/// <summary>
/// Location load manager to be attatched to the GameController GameObject of each location dependent 
/// </summary>
public class LoadManager : MonoBehaviour
{
    [SerializeField] [Tooltip("GameObjects to be enabled once LocationServices are connected")] List<GameObject> locationServiceDependent;
    bool connected = false;

    void Awake()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            Permission.RequestUserPermission(Permission.FineLocation);
        #endif

        ManageIncorrectScene();
    }

    void Update()
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
    }

}
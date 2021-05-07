using UnityEngine;
using Google.Maps;
using Google.Maps.Coord;

public class PlayerGPSMover : MonoBehaviour
{
    // Movement
    private float Speed; // Player speed m/s
    private float WalkingSpeed = 2; // (x m/s movement speed)
    private float RunningSpeed = 5; // (x m/s movement speed)
    private float RunDistnace = 5; // Run at x meter
    private float TeleportDistance = 50; // Teleport at x meter

    // Location
    private LatLng LatLng;
    private float distanceFromGPS;

    // Links
    [SerializeField] private MapsService MapsService;
    private GameObject player;

    private void Awake()
    {
        ManageIncorrectScene();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Error handling
        if (player == null)
            Debug.LogWarning("PlayerGPSMover.cs: player not set!");
        if (MapsService == null)
            Debug.LogWarning("PlayerGPSMover.cs: MapsService not set!");
    }

    private void Update()
    {
        // Retrieve current location
        LatLng = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Calculate Avatar distance from actual location
        distanceFromGPS = Vector3.Distance(player.transform.position, MapsService.Projection.FromLatLngToVector3(LatLng));

        // Set movement type (walk, run, teleport)
        SetMovementType();

        // Move the player
        player.transform.position = Vector3.MoveTowards(player.transform.position, MapsService.Projection.FromLatLngToVector3(LatLng), Speed * Time.deltaTime); // NOTE: This was causing distant telepoting on mobile, but always seemed to return to actual position
    }

    private void SetMovementType()
    {
        // if (in walking distance)...
        if (distanceFromGPS < RunDistnace)
        {
            Speed = WalkingSpeed;
        }
        // else if (past running distance)...
        else if (distanceFromGPS > TeleportDistance)
        {
            // Teleport the player
            player.transform.position = MapsService.Projection.FromLatLngToVector3(LatLng);
        }
        // else if (in running distance)...
        else if (distanceFromGPS > RunDistnace)
        {
            Speed = RunningSpeed;
        }
    }

    /// <summary>
    /// Disables this component if on the incorrect scene
    /// </summary>
    private void ManageIncorrectScene()
    {
        // Get the active scene
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        // IF (Map or Interaction scene is not active)...
        if (scene.name == "Map" || scene.name == "Interaction")
        { }
        else
            gameObject.GetComponent<PlayerGPSMover>().enabled = false; // Disable this gameobject
    }
}

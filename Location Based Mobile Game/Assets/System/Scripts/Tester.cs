using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.Maps;
using Google.Maps.Coord;

public class Tester : MonoBehaviour
{
    // DEBUG / TESTING
    [SerializeField] private bool DesktopDebug;

    [SerializeField] private float Speed; // (1 m/s movement speed)
    [SerializeField] private float WalkingSpeed = 2; // (2 m/s movement speed)
    [SerializeField] private float RunningSpeed = 5; // (5 m/s movement speed)

    [SerializeField] private float RunDistnace = 5; // at 5 meter
    [SerializeField] private float TeleportDistance = 30; // at 20 meter

    // Location stuff
    public double Latitude;
    public double Longitue;
    public LatLng LatLng;

    // Links
    [SerializeField] private GameObject player;
    [SerializeField] private MapsService MapsService;

    private void Start()
    {
        // Setup referances
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (DesktopDebug)
            LatLng = new LatLng(Latitude, Longitue);
        else
            LatLng = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Move the player based on latitude and longitude
        //player.transform.position = MapsService.Projection.FromLatLngToVector3(LatLng); // V1 (player teleports to the location)
        //player.transform.position = MapsService.Projection.FromLatLngToVector3(LatLng) * maxMoveSpeed * Time.deltaTime;


        // ----- Move the player based on latitude and longitude

        // Avatar distance from GPS
        float distanceFromGPS = Vector3.Distance(player.transform.position, MapsService.Projection.FromLatLngToVector3(LatLng));

        if (distanceFromGPS < RunDistnace) // (in walking distance)
        {
            Speed = WalkingSpeed;
        }
        else if (distanceFromGPS > TeleportDistance) // (out running distance)
        {
            // Teleport the player
            player.transform.position = MapsService.Projection.FromLatLngToVector3(LatLng);
        }
        else if (distanceFromGPS > RunDistnace) // (in running distance)
        {
            Speed = RunningSpeed;
        }

        // Move the player
        player.transform.position = Vector3.MoveTowards(player.transform.position, MapsService.Projection.FromLatLngToVector3(LatLng), Speed * Time.deltaTime); // NOTE: This was causing distant telepoting on mobile, but always seemed to return to actual position
    }
}

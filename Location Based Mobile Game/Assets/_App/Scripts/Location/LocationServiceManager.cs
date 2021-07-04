using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LocationServiceManager
{
    public static bool Connecting = false; // Bool flag
    public static string ConnectionStatus = "LocationService not called";

    [SerializeField] private static float AccuracyInMeters = 5;

    public static IEnumerator ConnectLocationService()
    {
        ConnectionStatus = "LocationSerivce not initialised";
        Connecting = true;

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) // Location not enabled
        {
            ConnectionStatus = "LocationSerivce not enabled by user";
            Connecting = false;
            yield break;
        }

        // Start service before querying location
        Input.location.Start(AccuracyInMeters);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            ConnectionStatus = "Location Service Timed out";
            Connecting = false;
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            ConnectionStatus = "Location Service Unable to determine device location";
            Connecting = false;
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            ConnectionStatus = "Location Service connected";
        }
        Connecting = false;
    }

}


// Source: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LocationServiceManager
{
    // Bool flags
    public static bool connecting = false;

    public static IEnumerator ConnectLocationService()
    {
        connecting = true;
        Debug.Log("LocationSerivce not initialised");

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) // Location not enabled
        {
            Debug.LogError("LocationSerivce not enabled by user");
            connecting = false;
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

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
            Debug.LogError("Location Service: Timed out");
            connecting = false;
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Location Service: Unable to determine device location");
            connecting = false;
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
    }

}


// Source: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
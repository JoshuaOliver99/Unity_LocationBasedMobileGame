using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Android;
public class LocationServiceManager : MonoBehaviour
{
    public double latitude;
    public double longitude;

    // Debug canvas stats
    [SerializeField] private Text locationText;
    [SerializeField] private Text statusText;

    IEnumerator Start()
    {
        statusText.text = ("LocationSerivce not initialised");

        // From: https://www.youtube.com/watch?v=lY0ONhO5gUw&ab_channel=wolfscrygames (dk if it's needed)
        //if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        //{
        //    Permission.RequestUserPermission(Permission.FineLocation);
        //    Permission.RequestUserPermission(Permission.CoarseLocation);
        //}

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) // Location not enabled
        {
            statusText.text = ("LocationSerivce not enabled by user");
            //yield break; Break from void (why was this included)
        }

        // Start service before querying location
        Input.location.Start();
        statusText.text = ("LocationSerivce initialised");

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
            print("Timed out");
            statusText.text = ("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            statusText.text = ("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            statusText.text = ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }

    private void Update()
    {
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        locationText.text = ("lat: " + latitude + " long: " + longitude);
    }
}


// Source: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
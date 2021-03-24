using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Coord;
using Google.Maps.Event;
using Google.Maps;
using Maps.Shared;

public class OldMapLoader : MonoBehaviour
{
    [Tooltip("LatLng to load (must be set before hitting play).")]
    public LatLng LatLng = new LatLng(40.6892199, -74.044601);

    [Tooltip("The required MapsService component (must be set before hitting play).")]
    public MapsService mapsService;

    /// <summary>
    /// Use <see cref="MapsService"/> to load geometry.
    /// </summary>
    private void Start()
    {
        // Set real-world location to load.
        mapsService.InitFloatingOrigin(LatLng);

        // TEST: Set real-world location to load (using test values).
        //mapsService.InitFloatingOrigin(new LatLng(gameObject.GetComponent<DemoLocation>().latitude, gameObject.GetComponent<DemoLocation>().longitude));


        // Register a listener to be notified when the map is loaded.
        mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);

        // Load map with default options.
        mapsService.LoadMap(MapDefaults.DefaultBounds, MapDefaults.DefaultGameObjectOptions);

    }

    /// <summary>
    /// Example of OnLoaded event listener.
    /// </summary>
    /// <remarks>
    /// The communication between the game and the MapsSDK is done through APIs and event listeners.
    /// </remarks>
    public void OnLoaded(MapLoadedArgs args)
    {
        // The Map is loaded - you can start/resume gameplay from that point.
        // The new geometry is added under the GameObject that has MapsService as a component.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// UI functionality for the MyProperty phone app
/// </summary>
public class MyProperty : MonoBehaviour
{
    [Header("References")]
    // Links
    PlayerSaveData_SO playerSaveData;
    [SerializeField] GameObject PropertyPanelPrefab;
    List<GameObject> propertyRef = new List<GameObject>(); // Reference to instantiated properties

    void Start()
    {
        // Get References
        playerSaveData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");


        // foreach (owned property)...
        foreach (PropertySaveData_SO property in playerSaveData.OwnedProperty)
        {
            // Create a property panel instance
            GameObject pPanel = Instantiate(PropertyPanelPrefab, transform);

            // Rename
            pPanel.name = property.SceneName; 
            
            // Store reference
            propertyRef.Add(pPanel);

            // Update the UI for this property
            updatePropertyUI(property, true);
        }
    }

    /// <summary>
    ///  Set latitude and longitude to that of the players.
    /// </summary>
    /// <param name="property">The changing property</param>
    void Btn_SetLocation(PropertySaveData_SO property)
    {
        if (property.RemainingMoves > 0)
        {
            // Set the property location
            property.Latitude = Input.location.lastData.latitude;
            property.longitude = Input.location.lastData.longitude;

            // Reduce the RemainingMoves counter
            property.RemainingMoves--;

            // Update this property text
            updatePropertyUI(property, false);

            // if (moves > 1)
            // Disable the set location button
        }
        else
        {
            //infrom there are no moves remaining
        }

    }

    /// <summary>
    /// Set property latitude and longitude using a world-space position 
    /// </summary>
    /// <param name="property">The changing property</param>
    /// <param name="location">The world-space positon</param>
    void Btn_SetLocation(PropertySaveData_SO property, Vector3 location)
    {
        if (property.RemainingMoves > 0)
        {
            // Convert location into LatLng
            // ...

            // Set the property location
            //property.Latitude = LatLng.latitude;
            //property.longitude = LatLng .longitude;

            // Reduce the RemainingMoves counter
            property.RemainingMoves--;

            // Update this property text
            updatePropertyUI(property, false);
        }
        else
        {
            // infrom there are no moves remaining
        }

    }

    /// <summary>
    /// Updates the MyProperty UI for the given property
    /// </summary>
    /// <param name="property">The property to be updated</param>
    /// <param name="firstTime">If the property needs moving and button functions</param>
    void updatePropertyUI(PropertySaveData_SO property, bool firstTime)
    {
        GameObject pPanel = GameObject.Find(property.name);

        // Set text
        pPanel.transform.Find("SceneName").GetComponent<TextMeshProUGUI>().text = property.SceneName;
        pPanel.transform.Find("PropertyName").GetComponent<TextMeshProUGUI>().text = property.PropertyName;
        pPanel.transform.Find("Location").GetComponent<TextMeshProUGUI>().text = property.Latitude + " " + property.longitude;

        if (firstTime)
        {
            // Add Button functions
            pPanel.transform.Find("Btn_SetLocationHere").GetComponent<Button>().onClick.AddListener(() => Btn_SetLocation(property));
            
            // Move (OLD: done in instantiation now)
            //pPanel.transform.SetParent(transform);
            //pPanel.transform.localPosition = transform.localPosition;
        }
    }
}

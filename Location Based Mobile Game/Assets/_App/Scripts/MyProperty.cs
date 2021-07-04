using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyProperty : MonoBehaviour
{
    [Header("References")]
    PlayerSaveData_SO playerSaveData;
    [SerializeField] GameObject PropertyPanelPrefab;

    void Start()
    {
        // Assign References
        playerSaveData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");

        // foreach (owned property, spawn a property panel)...
        foreach (PropertySaveData_SO property in playerSaveData.OwnedProperty)
        {
            GameObject newPropertyPanel = Instantiate(PropertyPanelPrefab);

            newPropertyPanel.name = property.SceneName;

            // Set text
            newPropertyPanel.transform.Find("SceneName").GetComponent<TextMeshProUGUI>().text = property.SceneName;
            newPropertyPanel.transform.Find("PropertyName").GetComponent<TextMeshProUGUI>().text = property.PropertyName;
            newPropertyPanel.transform.Find("Location").GetComponent<TextMeshProUGUI>().text = property.Latitude + " " + property.longitude;

            // Add Button functions
            newPropertyPanel.transform.Find("Btn_SetLocationHere").GetComponent<Button>().onClick.AddListener(() => Btn_SetLocation(property) );

            // Move
            newPropertyPanel.transform.SetParent(transform);
            newPropertyPanel.transform.localPosition = transform.localPosition;
        }
    }

    /// <summary>
    ///  Set latitude and longitude to the players
    /// </summary>
    /// <param name="property"></param>
    void Btn_SetLocation(PropertySaveData_SO property)
    {
        if (property.RemainingMoves > 0)
        {
            // Set the property location
            property.Latitude = Input.location.lastData.latitude;
            property.longitude = Input.location.lastData.longitude;

            // Reduce the RemainingMoves counter
            property.RemainingMoves--;
        }
        else
        {
            //infrom there are no moves remaining
        }

    }

    /// <summary>
    /// Set property latitude and longitude using a world-space position 
    /// </summary>
    /// <param name="property"> The changing property </param>
    /// <param name="location"> The world-space positon </param>
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
        }
        else
        {
            // infrom there are no moves remaining
        }

    }
}

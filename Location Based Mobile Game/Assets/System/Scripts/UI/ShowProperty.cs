using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Google.Maps;
using Google.Maps.Coord;

public class ShowProperty : MonoBehaviour
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
            newPropertyPanel.transform.Find("SceneName").GetComponent<TextMeshProUGUI>().text = property.SceneName;
            newPropertyPanel.transform.Find("PropertyName").GetComponent<TextMeshProUGUI>().text = property.PropertyName;

            // Add Button functions
            newPropertyPanel.transform.Find("Btn_See").GetComponent<Button>().onClick.AddListener(Btn_SeeLocation);
            newPropertyPanel.transform.Find("Btn_SetNew").GetComponent<Button>().onClick.AddListener(Btn_SetNewLocation);

            // Move
            newPropertyPanel.transform.SetParent(transform);
            newPropertyPanel.transform.localPosition = transform.localPosition;
        }
    }

    void Update()
    {
       
    }

    void Btn_SeeLocation()
    {
        // lerp the camera x,z position to the x,z position of the property location
    }

    void Btn_SetNewLocation()
    {
        // hide the notification panel

        // save the position where the user next clicks 

        // - display a physical marker (big red pillar or something) at the positon
        // - convert positon to LatLng and write to screen

        // - Display a confirm and concel button
    }

    void TestForNewLocation()
    {
        // if (player selects a location)

        // display the selected location in text
        // display the selected location in the world

    }

}

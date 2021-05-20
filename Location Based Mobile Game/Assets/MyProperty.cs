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
            newPropertyPanel.transform.Find("SceneName").GetComponent<TextMeshProUGUI>().text = property.SceneName;
            newPropertyPanel.transform.Find("PropertyName").GetComponent<TextMeshProUGUI>().text = property.PropertyName;
            newPropertyPanel.transform.Find("Location").GetComponent<TextMeshProUGUI>().text = property.Latitude + " " + property.longitude;

            // Add Button functions
            newPropertyPanel.transform.Find("Btn_SetLocationHere").GetComponent<Button>().onClick.AddListener(() => Btn_SetLocationHere(property) );
            //newPropertyPanel.transform.Find("Btn_See").GetComponent<Button>().onClick.AddListener(Btn_SeeLocation);
            //newPropertyPanel.transform.Find("Btn_SetNew").GetComponent<Button>().onClick.AddListener(Btn_SetNewLocation);

            // Move
            newPropertyPanel.transform.SetParent(transform);
            newPropertyPanel.transform.localPosition = transform.localPosition;
        }
    }

    void Btn_SetLocationHere(PropertySaveData_SO property)
    {
        // if (property.RemainingMoves > 0)
        // {

        // Set the property location
        property.Latitude = Input.location.lastData.latitude;
        property.longitude = Input.location.lastData.longitude;

        // Reduce the RemainingMoves counter
        // property.RemainingMoves--
        
        // }
        // else
        // {
        // infrom there are no moves remaining
        // }

    }
}

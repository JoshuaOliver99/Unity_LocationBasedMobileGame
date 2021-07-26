using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Property Save Data", menuName = "Property/Data", order = 1)]
public class PropertySaveData_SO : ScriptableObject
{
    [Header("Data")]
    public string SceneName;
    public string PropertyName;

    // Ownership
    public bool IsOwned;
    [SerializeField] bool previouslyOwned;

    // Value
    [SerializeField] float purchasePrice;
    [SerializeField] float sellPrice;

    // NOTE: For intended inventory class maybe
    //[Header("Inventory")]
    //Inventory_SO inventory;
    //Inventory_SO deployedItems;
    // OR
    //Dictionary of inventoryItem.cs with index being inventory positon

    [Header("Location")]
    public double Latitude;
    public double longitude;
    public int RemainingMoves;

    [Header("Save Data")]
    [SerializeField] string key;

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        if (key == "")
            key = name;

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void ResetData()
    {
        PropertySaveData_SO[] allProperty = Resources.LoadAll<PropertySaveData_SO>("DefaultPropertyData/");
        PropertySaveData_SO propertyReset = null;

        foreach (PropertySaveData_SO p in allProperty)
            if (p.SceneName == SceneName)
                propertyReset = p;

        if (propertyReset != null)
        {
            PropertyName = propertyReset.PropertyName;
            IsOwned = propertyReset.IsOwned;
            previouslyOwned = propertyReset.previouslyOwned;
            purchasePrice = propertyReset.purchasePrice;
            sellPrice = propertyReset.sellPrice;
            Latitude = propertyReset.Latitude;
            longitude = propertyReset.longitude;
            RemainingMoves = propertyReset.RemainingMoves;
        }

        Save();
    }
}

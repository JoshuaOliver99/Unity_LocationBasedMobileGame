using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Property Save Data", menuName = "Property/Data", order = 1)]

public class PropertySaveData_SO : ScriptableObject
{
    [Header("Data")]
    public string PropertyName;
    public string SceneName;

    public bool IsOwned;
    [SerializeField] bool previouslyOwned;

    [SerializeField] float purchasePrice;
    [SerializeField] float sellPrice;

    //[Header("Inventory")]
    // Inventory_SO inventory;
    // Inventory_SO deployedItems;

    [Header("Location")]
    public double Latitude;
    public double longitude;

    [Header("Save Data")]
    [SerializeField] string key;

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    void OnDisable()
    {
        if (key == "")
            key = name;

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
}

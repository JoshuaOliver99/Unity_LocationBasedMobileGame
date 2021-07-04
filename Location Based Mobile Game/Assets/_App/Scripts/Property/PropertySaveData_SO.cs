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

    // NOTE: For intended inventory class
    //[Header("Inventory")]
    // Inventory_SO inventory;
    // Inventory_SO deployedItems;

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
        if (key == "")
            key = name;

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Property Save Data", menuName = "Property/Data", order = 1)]

public class PropertySaveData_SO : ScriptableObject
{
    // data
    [SerializeField] string propertyName;
    [SerializeField] string sceneName;

    [SerializeField] public bool isOwned;
    [SerializeField] bool previouslyOwned;

    [SerializeField] float purchasePrice;
    [SerializeField] float sellPrice;

    // Inventory_SO inventory;

    // location
    [SerializeField] double latitude;
    [SerializeField] double longitude;


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

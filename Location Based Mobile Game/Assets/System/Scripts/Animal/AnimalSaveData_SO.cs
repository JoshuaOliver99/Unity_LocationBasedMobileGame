using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Save Data", menuName = "Animal/Data", order = 1)]
public class AnimalSaveData_SO : ScriptableObject
{
    [Header("Persistance Data")]
    // NOTE: TESTING: could be kept
    [SerializeField] public int TimesSeen;

    [Header("Info")]
    [SerializeField] public string animalName;
    [SerializeField] public bool isPet;
    [SerializeField] public bool isCurrentPet;

    [Header("Stats")]
    [SerializeField] public int health;
    [SerializeField] public int fun;

    [SerializeField] public int hunger;
    [SerializeField] public int thirst;
    [SerializeField] public int tiredness;

    [Header("Traits")]


    [Header("Skills")]

    
    [Header("Inventory")]


    [Header("Save Data")]
    [SerializeField] string key;


    private void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    private void OnDisable()
    {
        if (key == "")
            key = name;


        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
}

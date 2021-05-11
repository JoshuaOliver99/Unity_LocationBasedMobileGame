using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Save Data", menuName = "Animal/Data", order = 1)]
public class AnimalSaveData_SO : ScriptableObject
{
    [Header("Persistance Data")]
    //public string FirstDateMet;
    //public bool NeverSeen;
    
    public int TimesSeen;

    [Header("Info")]
    public string AnimalName;
    public bool IsPet;
    public bool WasPet;

    [Header("Stats")]
    public int Health;
    public int Bond;
    public int Fun;

    public int Hunger;
    public int Thirst;
    public int Filth;
    public int Tiredness;

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

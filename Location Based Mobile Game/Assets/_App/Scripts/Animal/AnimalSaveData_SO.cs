using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Save Data", menuName = "Animal/Data", order = 1)]
public class AnimalSaveData_SO : ScriptableObject
{

    [Header("Persistance Data")]
    //public string FirstDateMet; // UNUSED
    //public bool NeverSeen; // UNUSED
    public int TimesSeen; // UNUSED

    [Header("Info")]
    public string AnimalName;
    //public bool wasSeen; UNUSED
    public bool IsPet;
    public bool wasPet;
    
    // string species;
    // string breed;
    // MeshRenderer(?) petModel;

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

    public void ResetData()
    {
        Debug.LogError("AnimalSaveData_SO: ResetData() not yet implemented!");
        //AnimalSaveData_SO[] allAnimal = Resources.LoadAll<AnimalSaveData_SO>("DefaultAnimalData/");
        //AnimalSaveData_SO animalReset = null;
        //
        //foreach (AnimalSaveData_SO a in allAnimal)
        //    if (p.SceneName == SceneName)
        //        propertyReset = p;
        //
        //if (propertyReset != null)
        //{
        //    PropertyName = propertyReset.PropertyName;
        //    IsOwned = propertyReset.IsOwned;
        //    previouslyOwned = propertyReset.previouslyOwned;
        //    purchasePrice = propertyReset.purchasePrice;
        //    sellPrice = propertyReset.sellPrice;
        //    Latitude = propertyReset.Latitude;
        //    longitude = propertyReset.longitude;
        //    RemainingMoves = propertyReset.RemainingMoves;
        //}

        // Save();
    }

    public void Save()
    {

    }
}

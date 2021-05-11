using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Save Data", menuName = "Player/Data", order = 1)]

public class PlayerSaveData_SO : ScriptableObject
{
    [Header("Data")]
    // Date first played
    // Date and Time last played

    [Header("Inventory")]
    // Inventory_SO playerInventory
    // List<Inventory_SO> homeInventory

    [Header("Animals")]
    [SerializeField] public List<AnimalSaveData_SO> AllAnimals; // to display each animals save data
    [SerializeField] public List<AnimalSaveData_SO> CurrentPets;
    [SerializeField] List<AnimalSaveData_SO> previousPets;

    [Header("Save Data")]
    [SerializeField] string key;

    #region Loading
    public void LoadAnimals()
    {
        LoadAllAnimals();
        LoadCurrentPets();
        LoadPrevousPets();
    }

    /// <summary>
    /// Reloads all animals, providing the naming convention is adhered to
    /// </summary>
    void LoadAllAnimals()
    {
        AllAnimals.Clear();

        #region V1
#if false
        // (providing the naming convention is adhered to)...
        for (int i = 0; i >= 0; i++)
        {
            // if (the animal exists)...
            if (Resources.Load<AnimalSaveData_SO>("PetData/pet" + i) != null)
                animalSaveData.Add(Resources.Load<AnimalSaveData_SO>("PetData/pet" + i)); // Load it.
            else i = -1;
        }
#endif
        #endregion
        #region V2
        // Load all animals from resources folder into array...
        AnimalSaveData_SO[] animals = Resources.LoadAll<AnimalSaveData_SO>("PetData/");
        // foreach (animal in array)...
        foreach (AnimalSaveData_SO a in animals)
            AllAnimals.Add(a); // Load it.
        #endregion
    }

    /// <summary>
    /// Loads all current pets
    /// </summary>
    void LoadCurrentPets()
    {
        CurrentPets.Clear();

        // foreach (animal)...
        foreach (AnimalSaveData_SO animalSaveData in AllAnimals)
            if (animalSaveData.IsPet)
                CurrentPets.Add(animalSaveData);
    }
    
    /// <summary>
    /// Loads previous pets
    /// </summary>
    void LoadPrevousPets()
    {
        previousPets.Clear();

        // foreach (animal)...
        foreach (AnimalSaveData_SO animalSaveData in AllAnimals)
            if (animalSaveData.wasPet)
                previousPets.Add(animalSaveData);
    }
    #endregion

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);

        // Load Animal data
        LoadAnimals();
    }
    #region Saving

    void OnDisable()
    {
        if (key == "")
            key = name;

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
    #endregion Saving
}

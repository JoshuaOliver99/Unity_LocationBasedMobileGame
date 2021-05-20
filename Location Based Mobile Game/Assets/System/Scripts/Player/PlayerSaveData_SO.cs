using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Save Data", menuName = "Player/Data", order = 1)]

public class PlayerSaveData_SO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] bool isFirstPlay = true;
    [SerializeField] string firstPlayed; // UNUSED Date first played
    [SerializeField] string lastPlayed; // UNUSED Date last played

    [Header("Property")]
    public List<PropertySaveData_SO> Allproperty; // NOTE: UNUSED, To hold a list of all owend property scene names
    public List<PropertySaveData_SO> OwnedProperty; // NOTE: UNUSED, To hold a list of all owend property scene names

    [Header("Inventory")]
    [SerializeField] float balance; // NOTE: UNUSED
    // Inventory_SO playerInventory
    // List<Inventory_SO> propertyInventories

    [Header("Animals")]
    public List<AnimalSaveData_SO> AllAnimals; // to display each animals save data
    public List<AnimalSaveData_SO> CurrentPets;
    public List<AnimalSaveData_SO> previousPets;

    [Header("Save Data")]
    [SerializeField] string key;

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);

        
        if (isFirstPlay)
        {   
            // Save the first time played
            firstPlayed = System.DateTime.Now.ToString();
            isFirstPlay = false;
        }

        LoadAnimals();
        LoadProperty();
    }

    void OnDisable()
    {
        // Save the last time played
        lastPlayed = System.DateTime.Now.ToString();

        if (key == "")
            key = name;

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }



    #region Loading
    public void LoadAnimals()
    {
        LoadAllAnimals();
        LoadCurrentPets();
        LoadPrevousPets();
    }

    public void LoadProperty()
    {
        LoadAllProperty();
        LoadOwnedProperty();
    }


    // ----- Property
    void LoadAllProperty()
    {
        Allproperty.Clear();

        PropertySaveData_SO[] properties = Resources.LoadAll<PropertySaveData_SO>("PropertyData/");
        // foreach (property in array)...
        foreach (PropertySaveData_SO a in properties)
            Allproperty.Add(a); // Load it.
    }

    void LoadOwnedProperty()
    {
        OwnedProperty.Clear();

        // foreach (property)...
        foreach (PropertySaveData_SO a in Allproperty)
            if (a.IsOwned)
                OwnedProperty.Add(a);
    }


    // ----- Animals

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

        // foreach (animal, if (it's a pet))...
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



     


}

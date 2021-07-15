using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // scene dependent
    //[SerializeField] List<GameObject> animals; // UNUSED

    [Header("References")]
    PlayerSaveData_SO playerSaveData;
    GameObject player;
    [SerializeField] GameObject petPrefab;


    void Start()
    {
        Setup();

        playerSaveData.LoadAnimals();
        SpawnCurrentPets();

        playerSaveData.LoadProperty();
    }

    void Setup()
    {
        // Assign References
        playerSaveData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");
        player = GameObject.FindGameObjectWithTag("Player");
    }





    void SpawnCurrentPets()
    {
        foreach (AnimalSaveData_SO animalData in playerSaveData.CurrentPets)
        {
            // Spawn, set data, set name
            GameObject pet = Instantiate(petPrefab, player.transform.position, Quaternion.identity);
            pet.GetComponent<AnimalController>().animalData = animalData;
            pet.name = animalData.name;
        }
    }
}

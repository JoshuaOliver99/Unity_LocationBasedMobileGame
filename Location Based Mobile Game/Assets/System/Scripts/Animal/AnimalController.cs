using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

/// <summary>
/// Controls the behaviour of an animal
/// </summary>
public class AnimalController : MonoBehaviour
{
    [Header("Data")]
    string petID;

    [Header("References")]
    [SerializeField] public AnimalSaveData_SO animalData; // Animal save data

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;

    [Header("Movement")]
    [SerializeField] Vector3 anchor; // A point the animal is centeral to
    [SerializeField] float anchorRadius;
    [SerializeField] float maxRoam;

    [Header("Action")]
    [SerializeField] string action; // holds current behaviour, "" for nothing
    [SerializeField] float timer;
    [SerializeField] float maxIdle; // Max time the animal will idle


    void Start()
    {
        // Testing values
        #region TEST
        //anchorRadius = 10; // UNUSED ATM
        maxRoam = 5;

        //timer = 0;
        //maxIdle = 5;
        #endregion

        Setup();
    }

    private void Awake()
    {
        // NOTE: TESTING: testing to see if times seen increases and saves
    }

    bool updatedTimesSeen = false;
    void Update()
    {

        // NOTE: TEST: to check if data persists on application restart
        if (!updatedTimesSeen)
        {
            animalData.TimesSeen++;
            updatedTimesSeen = true;
        }

        Act();
    }


    #region SETUP
    private void Setup()
    {
        // Set data
        petID = name;

        // Assign references
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        // NOTE: Editor only: //animalData = AssetDatabase.LoadAssetAtPath<AnimalSaveData_SO>("Assets/System/Data/Pets/" + petID + ".asset");
        animalData = Resources.Load<AnimalSaveData_SO>("PetData/" + petID);
    }
    #endregion





    #region BEHAVIOUR
    private void Act()
    {
        // Check stat,
        // see if everything is healthy (e.g in the green)



        // if not already acting,

    }

    private void MoveTo(Vector3 location, float speed)
    {
        
    }

    /// <summary>
    /// Begin moving to a random point within maxRoam range from the animal and within anchorRadius
    /// </summary>
    private void Roam()
    {
        // NOTE:
        // ERROR, this does not stay within anchorRadius 

        agent.SetDestination(
            transform.position 
            + new Vector3(Random.Range(-maxRoam, maxRoam), 0, Random.Range(-maxRoam, maxRoam)));
    }

    /// <summary>
    ///  An animals response to the player whistling
    /// </summary>
    public void HearWhistle()
    {
        if (animalData.isPet)
        {
            // NOTE:
            // Include animal disobedience 

            // Go to the player
            agent.SetDestination(
                player.transform.position
                + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        }
        else
        {
            // Look at the player
            transform.LookAt(player.transform.position);
        }
    }
    #endregion


    #region TECHNICAL
    private void UpdateAnchor()
    {
        if (animalData.isPet)
            anchor = player.transform.position;
    }
    #endregion
}

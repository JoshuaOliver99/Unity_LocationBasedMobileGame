using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behaviour of an animal
/// </summary>
public class AnimalController : MonoBehaviour
{
    // References
    private PetData petData;
    private NavMeshAgent agent;
    private GameObject player;

    // Movement
    [SerializeField] private Vector3 anchor; // A point the animal is centeral to
    [SerializeField] private float anchorRadius;
    [SerializeField] private float maxRoam;

    // Action
    [SerializeField] private string action; // holds current behaviour, "" for nothing
    [SerializeField] private float timer;
    [SerializeField] private float maxIdle; // Max time the animal will idle


    void Start()
    {
        // Testing values
        #region TEST
        anchorRadius = 10; // UNUSED ATM
        maxRoam = 5;

        timer = 0;
        maxIdle = 5;
        #endregion


        Setup();
    }

    void Update()
    {
        Act();
    }


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
        if (petData.pet)
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





    #region TECHNICALs
    private void UpdateAnchor()
    {
        if (petData.pet)
            anchor = player.transform.position;
    }
    #endregion


    #region SETUP
    private void Setup()
    {
        // Assign references
        petData = gameObject.GetComponent<PetData>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion
}

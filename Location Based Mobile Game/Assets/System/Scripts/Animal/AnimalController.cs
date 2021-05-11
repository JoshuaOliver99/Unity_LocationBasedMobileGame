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

    [SerializeField] Vector3 newPos;

    [Header("Action")]
    // "Idle", "Roaming"
    [SerializeField] string action;

    [SerializeField] float idleTimer;

    [SerializeField] float idleLimit; // Time limit the animal will idle
    [SerializeField] float idleMax; // Max time the animal can idle


    void Start()
    {
        // Manual set data
        // NOTE: These should be be computed
        anchorRadius = 10;
        maxRoam = 5;

        idleMax = 3; // (dependant on traits (e.g. lazy) and stats)

        Setup();
    }

    void Update()
    {
        UpdateAnchor();

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
        animalData = Resources.Load<AnimalSaveData_SO>("PetData/" + petID);

        // Error check references
        if (agent == null)
            Debug.LogError(name + " No NavMeshAgent referance");
        if (player == null)
            Debug.LogError(name + " No Player referance");
        if (animalData == null)
            Debug.LogError(name + " No AnimalSaveData_SO referance");

        // Set components
        //transform.GetChild(0) // (Model GameObject)
    }
    #endregion
    



    #region BEHAVIOUR
    private void Act()
    {
        // Check stat,
        // see if everything is healthy (e.g in the green)

        // if not already acting,


        // If (agent has no path & not already idle)...
        if (!agent.hasPath && action != "Idle")
        {
            // Ensure no path is set
            agent.ResetPath();

            // Set a new idle time
            idleLimit = Random.Range(0f, idleMax);

            

            // If (Animal out of anchorRadius)
            if (Vector3.Distance(transform.position, anchor) > anchorRadius)
            {
                // Calculate how far OOB
                float distanceOOB = Vector3.Distance(newPos, anchor) - Vector3.Distance(transform.position, anchor);

                // Ensure the value is posotive
                if (distanceOOB < 0)
                    distanceOOB = -distanceOOB;

                // Move back towards the anchor, the distance OOB plus half the anchor radius
                agent.SetDestination(Vector3.MoveTowards(transform.position, anchor, distanceOOB + (anchorRadius / 2) ));
            }
            else
            {
                action = "Idle";
            }
        }

        // If (Idle)...a
        if (action == "Idle")
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > idleLimit)
            {

                if (!agent.hasPath)
                { 
                    LocalRoam();
                    idleTimer = 0;
                }
            }
        }

    }


    /// <summary>
    /// Begin moving to a random point within maxRoam range from the animal
    /// </summary>
    private void LocalRoam()
    {
        action = "Roaming";

        agent.SetDestination(transform.position + new Vector3(Random.Range(-maxRoam, maxRoam), 0, Random.Range(-maxRoam, maxRoam)));

        // Get a new point nearby
        //Vector3 newPos = transform.position + new Vector3(Random.Range(-maxRoam, maxRoam), 0, Random.Range(-maxRoam, maxRoam));
        //
        //// If (newPos is in anchorRadius)...
        //if (Vector3.Distance(newPos, anchor) < anchorRadius)
        //{
        //
        //    Debug.LogWarning(animalData.AnimalName + "position in bounds, Distance: " + Vector3.Distance(newPos, anchor));
        //    agent.SetDestination(newPos);
        //}
        //else if (Vector3.Distance(newPos, anchor) > anchorRadius * 2)
        //{
        //    //Vector3.MoveTowards(transform.position, anchor, anchorRadius / 2);
        //    Debug.LogWarning(animalData.AnimalName + "position far OOB, Distance: " + Vector3.Distance(newPos, anchor));
        //}
        //else
        //{
        //    Debug.LogWarning(animalData.AnimalName + "position OOB, Distance: " + Vector3.Distance(newPos, anchor));
        //}

    }


    /// <summary>
    ///  An animals response to the player whistling
    /// </summary>
    public void HearWhistle()
    {
        action = "Heard Whistle";

        if (animalData.IsPet)
        {
            // NOTE:
            // Include animal disobedience 

            // Go to nearby player
            agent.SetDestination(player.transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
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
        if (animalData.IsPet)
            anchor = player.transform.localPosition;
    }
    #endregion
}

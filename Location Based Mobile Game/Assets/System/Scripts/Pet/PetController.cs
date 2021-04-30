using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    // TESTING (everything needs access levels n such
    [SerializeField] float timer = 0;
    [SerializeField] float moveTimer = 5;
    //[SerializeField] float timeLimit = 10;
    [SerializeField] bool hasDestination = true;

    [SerializeField] float roamRange = 5; // detault value (probs change)
    [SerializeField] float maxRoamRange;
    
    Vector3 petBase;
    NavMeshAgent agent; // (Speed,
    [SerializeField] GameObject player;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        petBase = player.transform.position;
    }

    void Update()
    {
        if (!hasDestination)
            timer += Time.deltaTime;


        // Idle Roam (to random point within range from the pet)
        if (timer >= moveTimer && !hasDestination)
        {
            // Find potential new position
            Vector3 newPos = (transform.position + new Vector3(Random.Range(0, roamRange), 0, Random.Range(0, roamRange)));

            if (Vector3.Distance (newPos, petBase) < maxRoamRange) // If (newPos is within max range from the pet base)
            {
                agent.SetDestination(newPos); // Set the destination
                hasDestination = true; // Not at destination
            }
        }

        TestAtDestination();

        // PLAYER WHISTLE (SHOULD BE A PLAYER ACTION)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Call all pets near
            agent.SetDestination(player.transform.position + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2)));
            hasDestination = true;
        }

        print(agent.pathStatus);
    }

    void TestAtDestination()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && hasDestination)
        {
            hasDestination = false;
            timer = 0; // Reset movement timer
        }
    }
    
}

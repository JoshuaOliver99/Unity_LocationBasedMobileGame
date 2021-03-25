using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    // TESTING
    [SerializeField] int timer = 5;
    [SerializeField] float timePassed = 0;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= timer)
        {
            agent.SetDestination(transform.position + new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5)));
            timePassed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Call all pets near
            agent.SetDestination(player.transform.position + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2)));
            timePassed = 0;
        }
    }
}

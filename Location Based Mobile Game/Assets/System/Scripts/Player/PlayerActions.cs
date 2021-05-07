using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The abilities the player is able to act
/// </summary>
public class PlayerActions : MonoBehaviour
{
    // Refereneces
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> animals;
    [SerializeField] private List<GameObject> pets;

    // Data
    [SerializeField] private float speed;


    // TEST
    NavMeshAgent playerAgent;


    void Start()
    {
        // ----- Assign references

        // get all animals
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Animal"))
            animals.Add(g);

        player = GameObject.FindGameObjectWithTag("Player");


        // ----- Set data
        speed = 2f;
    }


    #region ACTIONS
    public void MoveForward()
    {
        // Version 1
        // Note: flies through everything
        //player.transform.position = player.transform.position + Camera.main.transform.forward * speed * Time.deltaTime;

        // Version 2
        MovePlayer(speed);
    }

    public void MoveBack()
    {
        MovePlayer(-speed);
    }

    /// <summary>
    /// Moves the player in the direction the main camera is facing, 
    /// providing that the new position is a valid NavMesh position.
    /// </summary>
    /// <remarks> Use negative speed for inverse direction </remarks>
    private void MovePlayer(float speed)
    {
        // set the desired new position
        Vector3 newPosition = Vector3.MoveTowards(player.transform.position, player.transform.position + Camera.main.transform.forward, speed * Time.deltaTime);

        // If (newPosition is a valid NavMesh position)...
        if (NavMesh.SamplePosition(newPosition, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            player.transform.position = navHit.position; // Move the player
    }

    public void Whistle()
    {
        foreach(GameObject g in animals)
            g.GetComponent<AnimalController>().HearWhistle();
    }
    #endregion
}

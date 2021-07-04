using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The abilities the player is able to act
/// </summary>
public class PlayerActions : MonoBehaviour
{
    [Header("References")]
    GameObject player; // The player
    List<GameObject> animals; // All animals in scene
    //List<GameObject> pets; // UNUSED, All pets in scene

    [Header("Data")]
    [SerializeField] float moveSpeed; // Player moveSpeed

    void Start()
    {
        getReferences();
        errorHandling();
    }

    #region SETUP
    void getReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // get all animals
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Animal"))
            animals.Add(g);
    }

    void errorHandling()
    {
        if (moveSpeed <= 0)
            Debug.LogWarning(name + ": PlayerActions.cs: assigned moveSpeed can't be <= 0");
    }
    #endregion


    #region ACTIONS
    public void MoveForward()
    {
        MovePlayer(moveSpeed);
    }

    public void MoveBack()
    {
        MovePlayer(-moveSpeed);
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

        // if (newPosition is a valid NavMesh position)...
        if (NavMesh.SamplePosition(newPosition, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            player.transform.position = navHit.position; // Move the player
    }

    public void Whistle()
    {
        foreach(GameObject a in animals)
            a.GetComponent<AnimalController>().HearWhistle();
    }
    #endregion
}

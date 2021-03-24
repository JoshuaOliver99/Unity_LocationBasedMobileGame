using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManualMover : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlayerController : MonoBehaviour
{
    private float moveSpeed = 1;
    private float rotateSpeed = 50;
    [SerializeField] private float personWidth;
    [SerializeField] private float personHeight;

    void Start()
    {
        Camera.main.transform.SetParent(gameObject.transform); // Steal the main camera
        Camera.main.transform.localPosition = new Vector3(-personWidth/2, personHeight-0.15f, personWidth/2); // To eye height
        //Camera.main.transform.localPosition = Vector3.zero; // Zero transform
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.position += -transform.right * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * Time.deltaTime * moveSpeed;
        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, Time.deltaTime * -rotateSpeed, 0);
        }

    }
}

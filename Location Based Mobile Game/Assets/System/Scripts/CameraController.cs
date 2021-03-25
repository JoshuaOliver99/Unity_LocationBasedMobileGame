using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Controller to be attatched to the camera base GameObject.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] bool isClose; // If the camera is close (not map)
    [SerializeField] bool isRotating = false;
    [SerializeField] bool isFirstPerson = false;
    [SerializeField] bool isCrouched = false;

    GameObject target;
    GameObject cam;
    [SerializeField] public GameObject playerModel;

    [SerializeField] int maxHeight = 10; 
    [SerializeField] int minHeight = 1;
    [SerializeField] int rotateSpeed = 20;
    [SerializeField] int ySpeed = 10;
    [SerializeField] bool isTargeting = true;

    [SerializeField] public Vector3 camOffset = new Vector3(0, 5, 10);
    [SerializeField] Vector3 standingHeight;
    [SerializeField] Vector3 crouchedHeight;

    void Start()
    {
        target = gameObject;

        cam = transform.GetChild(0).gameObject;
        cam.transform.position = camOffset;
    }

    void Update()
    {
        // Face the target
        if (isTargeting)
            cam.transform.LookAt(target.transform);

        if (isRotating)
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

        // Switch view positon
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchView();

            // Switch to another target
            // E.g. a shop, a competition, or other neay-by point of interest
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRotating = !isRotating; // Inverse rotating status
        }

        if (isFirstPerson && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;

            if (isCrouched)
                cam.transform.position = transform.position + crouchedHeight; // set the camera to the player crouched
            else
                cam.transform.position = transform.position + standingHeight; // set the camera to the player 
        }




        // First person controls
        if (isFirstPerson)
        {
            // Yaw rotation
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
            else if (Input.GetKey(KeyCode.D))
                transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
            // Pitch rotation
            else if (Input.GetKey(KeyCode.W))
                transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
            else if (Input.GetKey(KeyCode.S))
                transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
        }
        // Third person controls
        else if (!isFirstPerson)
        {
            // Horizontal orbiting
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
            else if (Input.GetKey(KeyCode.D))
                transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));

            // Y movement
            if (Input.GetKey(KeyCode.Q) && cam.transform.position.y > minHeight)
                cam.transform.position += new Vector3(0, -ySpeed * Time.deltaTime, 0);
            else if (Input.GetKey(KeyCode.E) && cam.transform.transform.position.y < maxHeight)
                cam.transform.position += new Vector3(0, ySpeed * Time.deltaTime, 0);
        }
    }

    void switchView()
    {
        isFirstPerson = !isFirstPerson; // Inverse mode
        isTargeting = !isTargeting;

        if (isFirstPerson)
            enterFirstPerson();
        else
            enterThirdPerson();
    }

    void enterFirstPerson()
    {
        playerModel.SetActive(false); // Disable the player model
        isCrouched = false; // Not crouching
        cam.transform.position = transform.position + standingHeight; // Set the camera to the player 
    }
    
    void enterThirdPerson()
    {
        playerModel.SetActive(true); // Enable the player model
        transform.rotation = Quaternion.identity; // Reset the rotation
        cam.transform.position = camOffset; // Reset the camera offset
        
    }
}

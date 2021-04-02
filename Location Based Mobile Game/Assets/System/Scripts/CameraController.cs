using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Controller to be attatched to the camera base GameObject (Camera parent).
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] bool SwitchingAllowed = true; // If camera can switch, true default
    [SerializeField] bool isRotating = false;
    [SerializeField] bool isTargeting = true;
    [SerializeField] bool isFirstPerson = false;
    [SerializeField] bool isCrouched = false;

    [SerializeField] GameObject target;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject playerModel;

    [SerializeField] int maxHeight = 10; 
    [SerializeField] int minHeight = 1;
    [SerializeField] int rotateSpeed = 10;
    [SerializeField] int ySpeed = 10;

    [SerializeField] Vector3 camOffset = new Vector3(0, 5, 10);
    [SerializeField] float standingHeight;
    [SerializeField] float crouchedHeight;


    void Start()
    {
        target = gameObject;

        cam = transform.GetChild(0).gameObject;
        cam.transform.position = camOffset;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Map")
            SwitchingAllowed = false;
    }

    void Update()
    {
        // Face the target
        if (isTargeting)
            cam.transform.LookAt(target.transform);

        // Rotate
        if (isRotating)
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));




        // Camera controls
        if (isFirstPerson)
            FirstPersonControls();
        else
        {
            ThirdPersonControls();

            // Toggle rotating
            if (Input.GetKeyDown(KeyCode.R))
                isRotating = !isRotating; // Inverse rotating status
        }

        // Switch view mode
        if (Input.GetKeyDown(KeyCode.Space) && SwitchingAllowed)
            SwitchView();

        // Switch crouched status
        if (isFirstPerson && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched; // Switch crouched status

            if (isCrouched)
                cam.transform.position = transform.position + new Vector3(0, crouchedHeight, 0); // Set to Crouched height
            else
                cam.transform.position = transform.position + new Vector3(0, standingHeight, 0); // Set to standing height
        }
    }

    void SwitchView()
    {
        isFirstPerson = !isFirstPerson; // Switch view mode
        isTargeting = !isTargeting; // Start or stop targeting
        isRotating = false; // Stop rotating
        playerModel.SetActive(!isFirstPerson); // Deactivate or activate player model


        if (isFirstPerson)
        {
            isCrouched = false; // Not crouching

            cam.transform.position = transform.position + new Vector3(0, standingHeight, 0); // Reset camera position
            cam.transform.rotation = new Quaternion(); // Reset camera rotation
        }
        else
        {
            transform.rotation = Quaternion.identity; // Reset the rotation
            cam.transform.position = transform.position + camOffset; // Reset the camera offset
        }
    }

    private void FirstPersonControls()
    {
        // Yaw rotation
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

        // Pitch rotation
        if (Input.GetKey(KeyCode.W))
            cam.transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
        else if (Input.GetKey(KeyCode.S))
            cam.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
    }

    private void ThirdPersonControls()
    {
        // Horizontal orbiting
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));

        // Y movement
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > minHeight)
            cam.transform.position += new Vector3(0, -ySpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.W) && cam.transform.transform.position.y < maxHeight)
            cam.transform.position += new Vector3(0, ySpeed * Time.deltaTime, 0);
    }

}

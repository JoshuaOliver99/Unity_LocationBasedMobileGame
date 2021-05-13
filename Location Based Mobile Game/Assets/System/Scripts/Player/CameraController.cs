using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Controller to be attatched to the camera base GameObject (Camera parent).
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject playerModel;

    [Header("Data")]
    [SerializeField] bool SwitchingAllowed = true; // If camera can switch, true default
    bool isRotating = false; // NOTE: Not used in touch controls
    bool isTargeting = true;
    bool isFirstPerson = false;
    bool isCrouched = false; // NOTE: Not used in touch controls

    [Header("Keyboard")]
    [SerializeField] float KeyboardRotateSpeed;
    [SerializeField] float KeyboardYSpeed;

    [Header("Touch")]
    [SerializeField] float TouchRotateSpeed;
    [SerializeField] float TouchYSpeed;

    [Header("Camera Locations")]
    [SerializeField] Vector3 camOffset = new Vector3(0, 5, 10);
    [SerializeField] float standingHeight;
    [SerializeField] float crouchedHeight;
    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;



    void Start()
    {
        target = gameObject;

        cam = transform.GetChild(0).gameObject;
        cam.transform.position = camOffset;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Map")
            SwitchingAllowed = false;

        // TESTING TO MOVE GAMEOBJECT
        gameObject.transform.SetParent(transform.parent.parent);
    }

    void Update()
    {
        // Face the target
        if (isTargeting)
            cam.transform.LookAt(target.transform);

        // Rotate
        if (isRotating)
            transform.Rotate(new Vector3(0, TouchRotateSpeed * Time.deltaTime, 0));


        // ----- Camera controls

        // if (first person)...
        if (isFirstPerson)
        {
            TouchFirstPersonControls();
            KeyboardFirstPersonControls(); // NOTE: can be removed on build
        }
        // else (third person)...
        else
        {
            TouchThirdPersonControls();
            KeyboardThirdPersonControls(); // NOTE: can be removed on build
        }

    }



    private void KeyboardFirstPersonControls()
    {
        // Yaw rotation (look left/right)
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -TouchRotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, TouchRotateSpeed * Time.deltaTime, 0));

        // Pitch rotation (look up/down)
        if (Input.GetKey(KeyCode.W))
            cam.transform.Rotate(new Vector3(-TouchRotateSpeed * Time.deltaTime, 0, 0));
        else if (Input.GetKey(KeyCode.S))
            cam.transform.Rotate(new Vector3(TouchRotateSpeed * Time.deltaTime, 0, 0));

        // Toggle crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched; // Switch crouched status

            if (isCrouched)
                cam.transform.position = transform.position + new Vector3(0, crouchedHeight, 0); // Set to Crouched height
            else
                cam.transform.position = transform.position + new Vector3(0, standingHeight, 0); // Set to standing height
        }
    }

    private void KeyboardThirdPersonControls()
    {
        // Horizontal orbiting (orbit left/right)
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -TouchRotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, TouchRotateSpeed * Time.deltaTime, 0));

        // Y movement (move up/down)
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > minHeight)
            cam.transform.position += new Vector3(0, -TouchYSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.W) && cam.transform.transform.position.y < maxHeight)
            cam.transform.position += new Vector3(0, TouchYSpeed * Time.deltaTime, 0);

        // Toggle rotating
        if (Input.GetKeyDown(KeyCode.R))
            isRotating = !isRotating; // Inverse rotating status
    }

    private void TouchFirstPersonControls()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Horizontal orbiting (orbit left/right)
            transform.Rotate(new Vector3(0, Input.GetTouch(0).deltaPosition.x, 0) * TouchRotateSpeed);

            // apply movment
            transform.Rotate(new Vector3(Input.GetTouch(0).deltaPosition.y, 0, 0) * TouchRotateSpeed);
        }
    }
    

    private void TouchThirdPersonControls()
    {
        // NOTE:
        // This is a crude implementation, done for leisure, drunk, in 20 min.
        // This can (and likely should) be done a lot smoother.

        // Initial movement test
        #region TEST1
        /*
        if (touch.phase == TouchPhase.Moved)
        {
            // Horizontal orbiting (orbit left/right)
            transform.Rotate(new Vector3(0, touch.deltaPosition.x, 0));

            // Y movement (move up/down)
            cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
        }
        */
        #endregion

        // Sensitivity edit, movement on one axis only
        #region TEST2
        /*
        if (touch.phase == TouchPhase.Moved)
        {
            // if (movement on the x axis)
            if (touch.deltaPosition.x > sensitivity || touch.deltaPosition.x < -sensitivity)
            {
                // Horizontal orbiting (orbit left/right)
                transform.Rotate(new Vector3(0, touch.deltaPosition.x, 0));
            }
            // else if (movement on the y axis)
            else if (touch.deltaPosition.y > sensitivity || touch.deltaPosition.y < -sensitivity)
            {
                // Y movement (move up/down)
                cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
            }
        }
        */
        #endregion

        // Added min and max y height
        // - The camera could go below the floor
        // - The camera is too hard to move, the user has to do big swipes
        #region TEST3
        //if (touch.phase == TouchPhase.Moved)
        //{
        //    // if (movement on the x axis)...
        //    if (touch.deltaPosition.x > sensitivity || touch.deltaPosition.x < -sensitivity)
        //    {
        //        // Horizontal orbiting (orbit left/right)
        //        transform.Rotate(new Vector3(0, touch.deltaPosition.x, 0));
        //    }
        //    // else if (movement on the y axis)...
        //    else if (touch.deltaPosition.y > sensitivity || touch.deltaPosition.y < -sensitivity)
        //    {
        //        // Y movement (move up/down)
        //        // if (y movement is negitive && cam position is more than min)...
        //        if (touch.deltaPosition.y < 0 && cam.transform.position.y > minHeight)
        //            cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
        //        // if (y movement is posotive && cam position is less than max)...
        //        else if (touch.deltaPosition.y > 0 && cam.transform.position.y < maxHeight)
        //            cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
        //    }
        //}
        #endregion


        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            transform.Rotate(new Vector3(0, Input.GetTouch(0).deltaPosition.x, 0) * TouchRotateSpeed);

            // if (movement out of bounds)
            if (cam.transform.position.y < minHeight)
                cam.transform.position = new Vector3(cam.transform.position.x, minHeight, cam.transform.position.z);
            else if (cam.transform.position.y > maxHeight)
                cam.transform.position = new Vector3(cam.transform.position.x, maxHeight, cam.transform.position.z);

            // apply movment
            cam.transform.position += new Vector3(0, Input.GetTouch(0).deltaPosition.y, 0) * TouchYSpeed;
        }
        
    }

    public void SwitchView()
    {
        if (SwitchingAllowed)
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
    }
}

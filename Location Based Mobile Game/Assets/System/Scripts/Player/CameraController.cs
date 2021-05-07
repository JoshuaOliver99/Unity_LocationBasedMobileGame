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


        // ----- Camera controls

        // Switch view mode
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchView();

        // if (first person)
        if (isFirstPerson)
        {
            TouchFirstPersonControls();
            KeyboardFirstPersonControls(); // NOTE: can be removed on build

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
        // else (third person)
        else
        {
            TouchThirdPersonControls();
            KeyboardThirdPersonControls(); // NOTE: can be removed on build

            // Toggle rotating
            if (Input.GetKeyDown(KeyCode.R))
                isRotating = !isRotating; // Inverse rotating status
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

    private void KeyboardFirstPersonControls()
    {
        // Yaw rotation (look left/right)
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

        // Pitch rotation (look up/down)
        if (Input.GetKey(KeyCode.W))
            cam.transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
        else if (Input.GetKey(KeyCode.S))
            cam.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
    }

    private void KeyboardThirdPersonControls()
    {
        // Horizontal orbiting (orbit left/right)
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));

        // Y movement (move up/down)
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > minHeight)
            cam.transform.position += new Vector3(0, -ySpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.W) && cam.transform.transform.position.y < maxHeight)
            cam.transform.position += new Vector3(0, ySpeed * Time.deltaTime, 0);
    }

    private void TouchFirstPersonControls()
    {

    }

    private void TouchThirdPersonControls()
    {
        if (Input.touchCount > 0)
        {
            // NOTE:
            // This is a crude implementation, done for leisure, drunk, in 20 min.
            // This can (and likely should) be done a lot smoother.

            Touch touch = Input.GetTouch(0);

            int sensitivity = 10; // required deltaPosition before movement occurs (used to stop both axis moving at once)

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
            // ISSUES:
            // - The camera could go below the floor
            // - The camera is too hard to move, the user has to do big swipes
            #region TEST3
            if (touch.phase == TouchPhase.Moved)
            {
                // if (movement on the x axis)...
                if (touch.deltaPosition.x > sensitivity || touch.deltaPosition.x < -sensitivity)
                {
                    // Horizontal orbiting (orbit left/right)
                    transform.Rotate(new Vector3(0, touch.deltaPosition.x, 0));
                }
                // else if (movement on the y axis)...
                else if (touch.deltaPosition.y > sensitivity || touch.deltaPosition.y < -sensitivity)
                {
                    // Y movement (move up/down)
                    // if (y movement is negitive && cam position is more than min)...
                    if (touch.deltaPosition.y < 0 && cam.transform.position.y > minHeight)
                        cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
                    // if (y movement is posotive && cam position is less than max)...
                    else if (touch.deltaPosition.y > 0 && cam.transform.position.y < maxHeight)
                        cam.transform.position += new Vector3(0, touch.deltaPosition.y, 0);
                }
            }
            #endregion
        }

    }
}

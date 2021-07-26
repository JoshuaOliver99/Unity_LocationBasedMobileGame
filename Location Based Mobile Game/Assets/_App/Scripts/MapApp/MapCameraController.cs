using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    [Header("References")]
    GameObject cam;
    GameObject camRig;
    GameObject player;

    [Header("Data")]
    [SerializeField] float moveSpeed;


    void Start()
    {
        GetReferences();
        SetData();
    }

    #region Setup
    void GetReferences()
    {
        //cam = Camera.main.gameObject;
        camRig = gameObject;
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void SetData()
    {
        moveSpeed = 0.2f;
    }


    #endregion
    void Update()
    {
        KeyboardConrols();
        TouchControls();
    }

    void KeyboardConrols()
    {
        // Horizontal
        if (Input.GetKeyDown(KeyCode.A))
            transform.position += new Vector3(0, 0, 0);
        else if (Input.GetKeyDown(KeyCode.D))
            transform.position += new Vector3(0, 0, 0);

        // Vertical
        if (Input.GetKeyDown(KeyCode.W))
            transform.position += new Vector3(0, 0, 0);
        else if (Input.GetKeyDown(KeyCode.S))
            transform.position += new Vector3(0, 0, 0);
    }

    void TouchControls()
    {
        // if (there is a touch)
        if (Input.touchCount > 0)
        {

            // if (moving first touch)...
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                camRig.transform.position += new Vector3(Input.GetTouch(0).deltaPosition.x, 0, Input.GetTouch(0).deltaPosition.y) * moveSpeed;
                
                // enable a return to player button

            }
        }
    }



    void ReturnToPlayer()
    {
        transform.position = player.transform.position;
    }

    void SetParent(GameObject newParent)
    {
        transform.SetParent(newParent.transform);
    }

}

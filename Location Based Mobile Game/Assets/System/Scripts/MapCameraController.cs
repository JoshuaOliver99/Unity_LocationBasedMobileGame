using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    GameObject cameraRig;
    GameObject parent;
    [SerializeField] int maxHeight; 
    [SerializeField] int minHeight;
    [SerializeField] int rotateSpeed;
    [SerializeField] int ySpeed;
    [SerializeField] bool isTargeting = true;

    void Start()
    {
        parent = transform.parent.gameObject;
        cameraRig = parent;
    }

    void Update()
    {
        // Face the target
        if (isTargeting)
            transform.LookAt(parent.transform);

        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            parent.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            parent.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }

        // Y movement
        if (Input.GetKey(KeyCode.Q) && transform.position.y > minHeight)
        {
            transform.position += new Vector3(0, -ySpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E) && transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, ySpeed * Time.deltaTime, 0);
        }

        // Switch target
        if (Input.GetKey(KeyCode.Space))
        { 
            // Switch to another target
            // E.g. a shop, a competition, or other neay-by point of interest
        }
    }
}

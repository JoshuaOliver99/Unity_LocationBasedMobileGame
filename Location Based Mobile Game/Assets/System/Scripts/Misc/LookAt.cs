using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cause the GameObject this is attatched to, to look at a target object
/// </summary>
public class LookAt : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] bool isTargetting;

    void Update()
    {
        if (isTargetting)
            transform.LookAt(target.transform);
    }
}

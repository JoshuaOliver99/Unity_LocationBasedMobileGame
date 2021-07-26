using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollider : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float fadeAmmount;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);

        Color color = collision.gameObject.GetComponent<Material>().color;
        color.a = fadeAmmount;

        collision.gameObject.GetComponent<Material>().color = color;
    }

    void OnCollisionExit(Collision collision)
    {
        Color color = collision.gameObject.GetComponent<Material>().color;
        color.a = fadeAmmount;

        collision.gameObject.GetComponent<Material>().color = color;
    }
}
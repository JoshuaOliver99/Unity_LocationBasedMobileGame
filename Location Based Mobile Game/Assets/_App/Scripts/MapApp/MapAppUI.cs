using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAppUI : MonoBehaviour
{

    /// <summary>
    ///  Return camera to the player
    /// </summary>
    public void Btn_ReturnToPlayer()
    {
        GameObject camRig = Camera.main.transform.parent.gameObject;

        // TO DO: Make this lerp correctly
        //float time = 1;
        //float lerpPercent = 0;
        //
        //while (lerpPercent <= time)
        //    lerpPercent += Time.deltaTime;
        //
        //if (lerpPercent > 1)
        //    lerpPercent = 1;
        //
        //camRig.transform.localPosition = Vector3.Lerp(camRig.transform.localPosition, Vector3.zero, lerpPercent);


        camRig.transform.localPosition = Vector3.zero;
    }
}

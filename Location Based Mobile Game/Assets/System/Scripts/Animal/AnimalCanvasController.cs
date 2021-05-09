using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalCanvasController : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] AnimalSaveData_SO animalData;

    void Start()
    {
        // NOTE: Providing this is still the rigt order
        text = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    void Update()
    {
        // NOTE: this is in update as (presumably) is is not set in time for start()
        // Get animal data
        if (animalData == null)
            animalData = transform.GetComponentInParent<AnimalController>().animalData;

        // Update text
        text.text = animalData.animalName + " Stats: " + '\n' +
                    "health: " + animalData.health + '\n' +
                    "fun: " + animalData.fun + '\n' +
                    "hunger: " + animalData.hunger + '\n' +
                    "thirst: " + animalData.thirst + '\n' +
                    "tiredness: " + animalData.tiredness + '\n' +
                    "Times Seen: " + animalData.TimesSeen;
    }
}

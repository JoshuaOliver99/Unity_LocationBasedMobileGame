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
        // NOTE: Rudimental, works providing this is order
        text = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    void Update()
    {
        // NOTE: this is in update as (presumably) is is not set in time for start()
        // Get AnimalSaveData_SO referance
        if (animalData == null)
            animalData = transform.GetComponentInParent<AnimalController>().animalData;

        // Display Animal stats
        text.text = animalData.AnimalName + " Stats: " + '\n' +
                    "health: " + animalData.Health + '\n' +
                    "bond: " + animalData.Bond + '\n' +
                    "fun: " + animalData.Fun + '\n' +
                    "hunger: " + animalData.Hunger + '\n' +
                    "thirst: " + animalData.Thirst + '\n' +
                    "tiredness: " + animalData.Tiredness + '\n' +
                    "filth: " + animalData.Filth;

    }
}

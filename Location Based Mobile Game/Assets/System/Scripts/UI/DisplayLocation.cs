using UnityEngine;
using UnityEngine.UI;

public class DisplayLocation : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DisplayConnectionStatus : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = LocationServiceManager.ConnectionStatus;
    }
}

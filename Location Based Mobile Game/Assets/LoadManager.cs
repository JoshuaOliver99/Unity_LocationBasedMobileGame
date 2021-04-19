using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    //[SerializeField] private Text statusText1; unused atm
    //[SerializeField] private Text statusText2; unused atm
    [SerializeField] private Text errorText;

    private bool locationServiceConnected = false;

    void Start()
    {
        // Add event listeners
        playButton.onClick.AddListener(PlayButtonClicked);
    }

    void Update()
    {
        // Test for location services
        if (!locationServiceConnected) // LocationService not connected
        {
            if (Input.location.status == LocationServiceStatus.Running)
                locationServiceConnected = true;
            else
                errorText.text = ("Error: Location services are not avaliable.");
        }

        // Activate play button
        if (!playButton.gameObject.activeSelf 
            && locationServiceConnected)
        {
            TogglePlayButton();
        }

    }

    /// <summary>
    /// Switches active state of the PlayButton GameObect on the Loading scene
    /// </summary>
    public void TogglePlayButton()
    {
        if (playButton.gameObject.activeSelf) // Is active > Set inactive
            playButton.gameObject.SetActive(false);
        else if (!playButton.gameObject.activeSelf) // Not active > Set active
            playButton.gameObject.SetActive(true);
    }

    private void PlayButtonClicked()
    {
        // Load the Interaction scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Interaction");
    }
}

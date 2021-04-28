using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    // DEBUG
    [SerializeField] private bool debugOn;
    [SerializeField] private string debugHomeName;

    private string homeName;

    private void Start()
    {
        if (debugOn)
        {
            homeName = debugHomeName;
        }
    }

    #region Buttons
    public void Btn_LoadMap()
    {
        print("show map pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
    }

    public void Btn_LoadHome()
    {
        print("Home pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(homeName);
    }

    public void btn_LoadInteraction()
    {
        print("Interaction pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Interaction");
    }
    #endregion
}

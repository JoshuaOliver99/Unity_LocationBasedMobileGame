using UnityEngine;
public class  UIFunctions : MonoBehaviour
{
    // DEBUG
    [SerializeField] private bool UseManualHomeName;
    [SerializeField] private string ManualHomeName;

    private string homeName;

    private void Start()
    {
        if (UseManualHomeName)
        {
            homeName = ManualHomeName;
        }
        else
        {
            // NOTE:
            // Set homeName to the players current home name.
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

    public void Btn_LoadInteraction()
    {
        print("Interaction pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Interaction");
    }
    #endregion
}


using UnityEngine;
using UnityEngine.UI;
public class  UIFunctions : MonoBehaviour
{
    // DEBUG
    [SerializeField] bool UseManualHomeName;
    [SerializeField] string ManualHomeName;

    // Data
    string homeName; // The players home scene

    // References
    CameraController cameraController;
    PlayerActions playerActions;
    [SerializeField] private GameObject phone;


    [Header("Phone")]
    RectTransform phoneRectTransform;
    bool retrievingPhone = false;
    float lerpPercent = 0;
    float phoneSpeed;

    [Header("Movment")]
    bool movingForward = false;
    bool movingBack = false;
    

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

        // Get refernces
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponentInParent<CameraController>();
        playerActions = GameObject.FindGameObjectWithTag("GameController").transform.GetComponentInParent<PlayerActions>();

        // Phone rect transform
        phoneRectTransform = phone.GetComponent<RectTransform>();
    }

    private void Update()
    {
        movePlayer();
        updatePhonePos();
    }

    void movePlayer()
    {
        if (movingForward)
        {
            playerActions.MoveForward();
        }
        else if (movingBack)
        {
            playerActions.MoveBack();
        }
    }

    void updatePhonePos()
    {
        // Note:
        // lerpPercent should be clamped (0, 1),
        // but it still works because Lerp functions from 0 to 1

        // if (retrieving the phone & lerpPercent is less than max)...
        if (retrievingPhone && lerpPercent < 1)
        {
            lerpPercent += Time.deltaTime * phoneSpeed; // Increase lerpPercent
        }
        // if (not already retrieving the phone & lerpPercent is greater than min)...
        else if (!retrievingPhone && lerpPercent > 0)
        {
            lerpPercent -= Time.deltaTime * phoneSpeed; // Decrease lerpPercent
        }

        // Lerp the phone position
        phoneRectTransform.localPosition = Vector3.Lerp(new Vector3(0, -1700, 0), new Vector3(0, -10, 0), lerpPercent);
    }




    #region Buttons
    public void Btn_LoadMap()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
    }

    public void Btn_LoadHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(homeName);
    }

    public void Btn_LoadInteraction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Interaction");
    }

    public void Btn_Quit()
    {
        Application.Quit();
    }


    public void Btn_SwitchView()
    {
        cameraController.SwitchView();
    }


    public void Btn_SetMoveForward()
    {
        movingForward = true;    
    }
    public void Btn_UnsetMoveForward()
    {
        movingForward = false;
    }

    public void Btn_SetMoveBack()
    {
        movingBack = true;
    }    
    public void Btn_UnsetMoveBack()
    {
        movingBack = false;
    }


    public void Btn_ShowPhone()
    {
        retrievingPhone = true;
    }
    public void Btn_HidePhone()
    {
        retrievingPhone = false;
    }


    public void Btn_ShowActions()
    {
        // Shows or hides list of action buttons
    }

    public void Btn_Whistle()
    {
        playerActions.Whistle();
    }
    #endregion
}


using UnityEngine;
using UnityEngine.UI;
public class  UIFunctions : MonoBehaviour
{
    // DEBUG
    [SerializeField] private bool UseManualHomeName;
    [SerializeField] private string ManualHomeName;

    // Data
    private string homeName; // The players home scene

    // References
    private CameraController cameraController;
    private PlayerActions playerActions;
    [SerializeField] private GameObject phone;


    // TESTING
    // Phone
    private RectTransform phoneRectTransform;
    private bool retrievingPhone = false;
    [SerializeField] private float lerpPercent = 0;
    [SerializeField] private float phoneSpeed;


    // TESTING
    // Movment
    [SerializeField] private bool movingForward = false;
    [SerializeField] private bool movingBack = false;
    

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
        // ----- Moving the phone
        if (retrievingPhone && lerpPercent < 1)
        {
            // Note: this needs clamping between 0 and 1 (well it doesnt because its normal lerp actually but ygm)
            lerpPercent += Time.deltaTime * phoneSpeed;
        }
        else if (!retrievingPhone && lerpPercent > 0)
        {
            lerpPercent -= Time.deltaTime * phoneSpeed;
        }

        phoneRectTransform.localPosition = Vector3.Lerp(new Vector3(0, -1700, 0), new Vector3(0, -10, 0), lerpPercent);


        // ----- Movement detection
        if (movingForward)
        {
            playerActions.MoveForward();
        }
        else if (movingBack)
        {
            playerActions.MoveBack();
        }
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


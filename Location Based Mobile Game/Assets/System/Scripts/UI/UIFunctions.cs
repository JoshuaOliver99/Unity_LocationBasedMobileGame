using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Maps;
using Google.Maps.Coord;

public class  UIFunctions : MonoBehaviour
{
    [Header("References")]
    PlayerSaveData_SO playerSaveData;
    CameraController cameraController;
    PlayerActions playerActions;
    [SerializeField] private MapsService MapsService;


    [Header("Property")]
    List<PropertySaveData_SO> property;

    // Test
    // List of inrange property
    List<PropertySaveData_SO> propertyInRange;


    [Header("Phone")]
    [SerializeField] private GameObject phone;
    RectTransform phoneRectTransform;
    bool retrievingPhone = false;
    float lerpPercent = 0;
    [SerializeField] float phoneSpeed;
    [SerializeField] Vector3 phoneDownPos = new Vector3(0, -1700, 0);

    GameObject homeScreen;
    GameObject activeSceen;

    [Header("Movment")]
    bool movingForward = false;
    bool movingBack = false;
    

    void Start()
    {
        GetReferences();
        SetData();
        ErrorHandling();
    }

    #region SETUP
    void GetReferences()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponentInParent<CameraController>();
        playerActions = GameObject.FindGameObjectWithTag("GameController").transform.GetComponentInParent<PlayerActions>();

        playerSaveData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");
        property = playerSaveData.OwnedProperty;

        // Phone
        phoneRectTransform = phone.GetComponent<RectTransform>();
        homeScreen = phone.transform.Find("HomeScreen").gameObject;
    }

    void ErrorHandling()
    {
        if (phone == null)
            Debug.LogWarning(name + ": UIFuncations.cs: assigned phone can't be null");
        if (phoneSpeed <= 0)
            Debug.LogWarning(name + ": UIFuncations.cs: assigned phoneSpeed can't <= 0");
    }

    void SetData()
    {

    }
    #endregion


    void Update()
    {
        MovePlayer();
        UpdatePhonePos();
    }

    void MovePlayer()
    {
        if (movingForward)
            playerActions.MoveForward();
        else if (movingBack)
            playerActions.MoveBack();
    }

    void UpdatePhonePos()
    {
        // Note:
        // lerpPercent should be clamped (0, 1),
        // but it still works because Lerp functions from 0 to 1

        // if (retrieving the phone & lerpPercent is less than max)...
        if (retrievingPhone && lerpPercent < 1)
            lerpPercent += Time.deltaTime * phoneSpeed;
        // if (not already retrieving the phone & lerpPercent is greater than min)...
        else if (!retrievingPhone && lerpPercent > 0)
            lerpPercent -= Time.deltaTime * phoneSpeed;

        // Lerp the phone position
        phoneRectTransform.localPosition = Vector3.Lerp(phoneDownPos, Vector3.zero, lerpPercent);
    }




    #region Buttons
    public void Btn_LoadMap()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
    }

    public void Btn_LoadProperty()
    {
        propertyInRange.Clear();

        // Players position
        LatLng playerLatLng = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);

        foreach (PropertySaveData_SO property in playerSaveData.OwnedProperty)
        {
            // Property positon
            LatLng propertyLatLng = new LatLng(property.Latitude, property.longitude);

            // Calculate the distance
            Vector3 playerPos = new Vector3(MapsService.Projection.FromLatLngToVector3(playerLatLng).x, 0, MapsService.Projection.FromLatLngToVector3(playerLatLng).z);
            Vector3 propertyPos = new Vector3(MapsService.Projection.FromLatLngToVector3(propertyLatLng).x, 0, MapsService.Projection.FromLatLngToVector3(propertyLatLng).z);

            float GPSDistanceFromProperty = Vector3.Distance(playerPos, propertyPos);

            // Calculate if it's in range
            // NOTE: 20f should be changed
            if (GPSDistanceFromProperty < 20f)
            {
                propertyInRange.Add(property);
                Debug.LogError(property.name + " in range!");
            }
        }

        // TESTING
        // if (one property)...
        if(propertyInRange.Count == 1)
        {
            // Load that property
            UnityEngine.SceneManagement.SceneManager.LoadScene(propertyInRange[0].SceneName);
        }


        // ----- OLD -----
        //UnityEngine.SceneManagement.SceneManager.LoadScene(homeName);

        //double lat = Input.location.lastData.latitude;
        //double lng = Input.location.lastData.longitude;
    }

    public void Btn_LoadInteraction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Interaction");
    }

    public void Btn_Quit()
    {
        Application.Quit();
    }

    // Camera
    public void Btn_SwitchView()
    {
        cameraController.SwitchView();
    }

    // Movemnt
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

    // Phone
    public void Btn_ShowPhone()
    {
        retrievingPhone = true;
    }
    public void Btn_HidePhone()
    {
        retrievingPhone = false;
    }

    public void Btn_ShowScreen(GameObject screen)
    {
        activeSceen = screen;
        activeSceen.SetActive(true);
        homeScreen.SetActive(false);
    }

    public void Btn_GoHomeScreen()
    {
        activeSceen.SetActive(false);
        homeScreen.SetActive(true);
    }

    // Player actions
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


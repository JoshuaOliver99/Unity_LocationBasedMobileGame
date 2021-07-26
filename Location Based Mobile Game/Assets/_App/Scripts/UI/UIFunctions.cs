using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Maps;
using Google.Maps.Coord;
using TMPro;

/// <summary>
/// A class to house general UI functions
/// </summary>
public class UIFunctions : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]    TMP_Text DebugText; // For easier mobile testing

    [Header("References")]
    [SerializeField]    MapsService MapsService;
                        CameraController cameraController;
                        PlayerSaveData_SO playerSaveData;
                        PlayerActions playerActions;

    [Header("Property")]
    [SerializeField]    float MaxPropertyRange = 20f;
                        List<PropertySaveData_SO> propertyInRange = new List<PropertySaveData_SO>();

    [Header("Phone")]
    [SerializeField]    GameObject phone;
                        RectTransform phoneRectTransform;
                        bool retrievingPhone = false;
                        float lerpPercent = 0;
    [SerializeField]    float phoneSpeed = 3;
    [SerializeField]    Vector3 phoneDownPos = new Vector3(0, -1700, 0);

                        GameObject homeScreen;
                        GameObject activeSceen;

    [Header("Movment")]
                        bool movingForward = false;
                        bool movingBack = false;
    

    void Start()
    {
        GetReferences();
        ErrorHandling();
    }

    #region SETUP
    void GetReferences()
    {
        // cameraController
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponentInParent<CameraController>();
        if (cameraController == null)
            Debug.LogWarning("UIFunctions: cameraController > Find Tag 'MainCamera' failed.");

        // playerActions
        playerActions = GameObject.FindGameObjectWithTag("GameController").transform.GetComponentInParent<PlayerActions>();
        if (playerActions == null)
            Debug.LogWarning("UIFunctions: playerActions > Find Tag 'GameController' failed.");

        // playerSaveData
        playerSaveData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");
        if (cameraController == null)
            Debug.LogWarning("UIFunctions: playerSaveData > Load 'Resources/PlayerData/Player' failed.");
        //property = playerSaveData.OwnedProperty;

        // Phone
        phone = GameObject.Find("Phone");
        if (phone == null)
            Debug.LogWarning("UIFunctions: phone > Find 'phone' failed.");
        else
        {
            phoneRectTransform = phone.GetComponent<RectTransform>();  
            homeScreen = phone.transform.Find("HomeScreen").gameObject;
        }
    }

    void ErrorHandling()
    {
        if (phoneSpeed <= 0)
            Debug.LogWarning(name + ": UIFuncations.cs: assigned phoneSpeed can't <= 0");
    }

    #endregion


    void Update()
    {
        MovePlayer();
        if (phone != null)
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
        // NOTE:
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
    /// <summary>
    ///  Reset game saved data
    /// </summary>
    public void Btn_ResetData()
    {
        PlayerSaveData_SO playerData = Resources.Load<PlayerSaveData_SO>("PlayerData/Player");
        playerData.ResetData();

        PropertySaveData_SO[] properties = Resources.LoadAll<PropertySaveData_SO>("PropertyData/");
        for (int i = 0; i < properties.Length; i++)
            properties[i].ResetData();
        
        AnimalSaveData_SO[] animals = Resources.LoadAll<AnimalSaveData_SO>("AnimalData/");
        for (int i = 0; i < animals.Length; i++)
            animals[i].ResetData();

    }

    public void Btn_LoadTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

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
            //OLD: Vector3 playerPos = new Vector3(MapsService.Projection.FromLatLngToVector3(playerLatLng).x, 0, MapsService.Projection.FromLatLngToVector3(playerLatLng).z);
            //OLD: Vector3 propertyPos = new Vector3(MapsService.Projection.FromLatLngToVector3(propertyLatLng).x, 0, MapsService.Projection.FromLatLngToVector3(propertyLatLng).z);
            Vector3 playerPos = MapsService.Projection.FromLatLngToVector3(playerLatLng);
            Vector3 propertyPos = MapsService.Projection.FromLatLngToVector3(propertyLatLng);
            float GPSDistanceFromProperty = Vector3.Distance(playerPos, propertyPos);

            // Record in-range property
            if (GPSDistanceFromProperty < MaxPropertyRange)
                propertyInRange.Add(property);
        }

        // if (one property)...
        if(propertyInRange.Count == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene(propertyInRange[0].SceneName); // Load it
        else if (propertyInRange.Count > 1)
        {
            // TO DO:
            // Show all avaliable properties
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

    public void Btn_LoadOffline()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Offline");
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


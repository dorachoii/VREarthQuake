using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class EJRadioDial : MonoBehaviour
{
    #region HandRot Variables

    private Transform Hand;
    private Quaternion tempHand1;
    private Quaternion tempHand2;

    bool bRotate;

    #endregion

    #region gridLine Variables

    // public GameObject gridLine;
    // float gridLinePosX;
    // float gridLineStartPosX = 0.03f;
 
    // float gridLineEndPosX;
    #endregion

    #region SFX Variables
    // public AudioSource noise;
    // public AudioSource broadCast;
    // float noiseVolume;
    // float broadcastVolume;
    #endregion

    #region VRInput Variables

[SerializeField]
    //public XRIDefaultInputActions inputActions;
    public InputActionAsset inputActions;
    // public InputAction XRButtonPrimary;
    // public InputAction XRButtonSecondary;
    // public InputAction XRButtonTrigger;
    // public InputAction XRButtonGrip;
    

    #endregion

    #region VR InputState
    private void Awake()
    {
        //inputActions = new XRIDefaultInputActions();
        //inputActions = new XRSimulatedController();
        inputActions = gameObject.GetComponent<InputActionManager>().actionAssets[0];
        
    }

    // private void OnEnable()
    // {
    //     XRButtonPrimary = inputActions.EJXRInput.PrimaryBtn;
    //     XRButtonSecondary = inputActions.EJXRInput.SecondaryBtn;
    //     XRButtonGrip = inputActions.EJXRInput.GripBtn;
    //     XRButtonTrigger = inputActions.EJXRInput.TriggerBtn;

    //     XRButtonPrimary.Enable();
    //     XRButtonSecondary.Enable();
    //     XRButtonGrip.Enable();
    //     XRButtonTrigger.Enable();

    //     XRButtonTrigger.started += OnStarted;
    //     XRButtonTrigger.canceled += OnCanceled;
    // }

    // private void OnDisable()
    // {
    //     XRButtonTrigger.started -= OnStarted;
    //     XRButtonTrigger.canceled -= OnCanceled;
    // }

    enum HandState
    {
        None,
        Pressed,
        Pressing,
        Released,
    }

    HandState handstate;

    void OnStarted(CallbackContext context)
    {
        handstate = HandState.Pressed;
    }

    void OnCanceled(CallbackContext context) 
    {  
        handstate = HandState.Released; 
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // tempHand1 = inputActions.XRIRightHandInteraction.ActivateValue.ReadValue<Quaternion>();
        // Debug.Log($"test: Quaternion {tempHand1}");
        //tempHand1 = XRSimulatedController.rightHand;

        // noiseVolume = noise.volume;
        // broadcastVolume = broadCast.volume;
    }

    // Update is called once per frame
    void Update()
    {
     
    //    //var tempHand1 = inputActions.XRIRightHand.Rotation.activeValueType; //Quaternion이라고 뜸
    //     var tempHand1 = inputActions.XRIRightHand.Rotation.ReadValue<Quaternion>();
        
    //     Debug.Log($"test: Quaternion {tempHand1}");

    //     //if(XRButtonTrigger.triggered)
    //     if(inputActions.XRIRightHandInteraction.Activate.triggered)
    //     {
    //         Debug.Log($"test:: triggered");
    //         bRotate = true;
    //         //tempHand1.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);
    //     }

    //     if(inputActions.XRIRightHandInteraction.Activate.enabled)
    //     {
    //         Debug.Log($"test:: enabled");
    //         bRotate = true;
    //         //tempHand1.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);
    //     }

    //     if(inputActions.XRIRightHandInteraction.Activate.inProgress)
    //     {
    //         Debug.Log($"test:: inProgress");
    //         bRotate = true;
    //         //tempHand1.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);
    //     }

        

        // if (bRotate)
        // {
        //     //01. HandRot - dialRot
        //     tempHand2.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);

        //     float angle = Vector3.Angle(tempHand1.up, tempHand2.up);

        //     int dir = -1;

        //     if (Vector3.Dot(tempHand2.up, tempHand1.right) > 0)
        //     {
        //         dir = 1;
        //     }

        //     transform.Rotate(0, 0, angle * dir * 0.05f);


        //     //02.HandRot - GridLinePos
        //     float v = angle * dir * 0.005f;
        //     Vector3 gridDir = v * Vector3.right;

        //     Mathf.Clamp(gridLine.transform.position.x, -0.05f, 0.03f);
        //     gridLine.transform.position += gridDir * Time.deltaTime;

        //     //03.HandRot - NoiseVolume
        //     float volume = angle * dir * 0.05f;
        //     noise.volume +=volume;
        //     broadCast.volume -= volume;
           
        //     if (noise.volume < 0.2f)
        //     {
        //         // EJToDoManager.instance.TodolistON();
        //         // PJH_Notifiction.instance.RCD();
        //     }
        //     tempHand1.localEulerAngles = tempHand2.localEulerAngles;
        // }

        // if (XRButtonSecondary.triggered)
        // {
        //     bRotate = false;
        // }

    }

    void Rotate()
    {
        transform.Rotate(Vector3.up, 30f);
    }
}

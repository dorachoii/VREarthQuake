using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.InputSystem.InputAction;

public class EJXRToolkitInput3 : MonoBehaviour
{
    public XRIDefaultInputActions inputActions;
    public InputAction XRButtonPrimary;
    public InputAction XRButtonSecondary;
    public InputAction XRButtonTrigger;
    public InputAction XRButtonGrip;


    public TextMeshProUGUI textMeshPro;
    string text;

    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
    }

    private void OnEnable()
    {
        XRButtonPrimary = inputActions.EJXRInput.PrimaryBtn;
        XRButtonSecondary = inputActions.EJXRInput.SecondaryBtn;
        XRButtonGrip = inputActions.EJXRInput.GripBtn;
        XRButtonTrigger = inputActions.EJXRInput.TriggerBtn;

      
        XRButtonPrimary.Enable();
        XRButtonSecondary.Enable();
        XRButtonGrip.Enable();
        XRButtonTrigger.Enable();


        XRButtonPrimary.started += (CallbackContext context) =>
        {
            // One버튼 눌림
        };
        XRButtonPrimary.canceled += (CallbackContext context) =>
        {
            // One버튼 뗌

        };
    }
    // Start is called before the first frame update
    void Start()
    {
        text = textMeshPro.text;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axis = inputActions.XRILeftHandLocomotion.Move.ReadValue<Vector2>();


        //if (XRButtonPrimary.triggered)
        //{
        //    text = "Primary triggered";
        //    textMeshPro.text += text;
        //    print("Primary triggered");
        //}

        if (XRButtonSecondary.triggered)
        {
            text = "Seceondary triggerd";
            textMeshPro.text += text;
            print("Secondary triggered");
        }

        if (XRButtonGrip.triggered)
        {
            text = "Grip triggerd";
            textMeshPro.text += text;
            print("Grip triggered");
        }

        if (XRButtonTrigger.triggered)
        {
            text = "tigger triggerd";
            textMeshPro.text += text;
            print("trigger triggered");
        }

        //test 필요
        XRButtonPrimary.started += (CallbackContext context) =>
        {
            // One버튼 눌림
            text = "primary started";
            textMeshPro.text += text;
            print("primary started");

        };
        XRButtonPrimary.canceled += (CallbackContext context) =>
        {
            // One버튼 뗌
            text = "primary canceled";
            textMeshPro.text += text;
            print("primary canceled");
        };
        XRButtonPrimary.performed += (CallbackContext context) =>
        {
            // One버튼 뗌
            text = "primary performed";
            textMeshPro.text += text;
            print("primary performed");
        };

        if (XRButtonPrimary.inProgress)
        {
            text = "primary inprogress";
            textMeshPro.text += text;
            print("primary inprogress");
        }
    }
}



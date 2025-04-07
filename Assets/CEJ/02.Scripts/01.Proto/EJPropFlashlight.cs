using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EJPropFlashlight : EJPropBase
{
    public GameObject spotLight;

    #region VRInput Variables

    public XRIDefaultInputActions inputActions;
    public InputAction XRButtonPrimary;
    public InputAction XRButtonSecondary;
    public InputAction XRButtonTrigger;
    public InputAction XRButtonGrip;

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
    }

    #endregion
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        //if(XRButtonPrimary.triggered)
        {
            TurnONOFFLight();
            //DoAction();
        }
    }

    public override void DoAction()
    {
        base.DoAction();

        spotLight.SetActive(!spotLight.activeSelf);
        PJH_Notifiction.instance.Tablet();
    }

    public void TurnONOFFLight()
    {
        audiosource.PlayOneShot(propClip); 
        if (!spotLight.activeSelf)
        {
            spotLight.SetActive(true);
            PJH_Notifiction.instance.Tablet();
        }
        else
        {
            spotLight.SetActive(false);
        }
    }
}

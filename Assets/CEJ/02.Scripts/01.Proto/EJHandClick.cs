using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class EJHandClick : MonoBehaviour
{
    // Movable(click event 사물들)
    // secondaryBtn 누르면 Ray 발사 후, primaryBtn 누르면 작동
    // slipper, fusebox, doors 

    // Grabbable
    // GrabBtn 누르고 회전시 누적값 반영
    // ipad, velve, radio, flashlight

    public Transform rightHand;

    LineRenderer lr;
    LayerMask interactivePropsLayer;

    #region toolkit Input
    public XRIDefaultInputActions inputActions;
    public InputAction XRButtonPrimary;
    public InputAction XRButtonSecondary;
    public InputAction XRButtonTrigger;
    public InputAction XRButtonGrip;
    #endregion

    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
    }

    private void OnEnable()
    {
        #region toolkit Input
        XRButtonPrimary = inputActions.EJXRInput.PrimaryBtn;
        XRButtonSecondary = inputActions.EJXRInput.SecondaryBtn;
        XRButtonGrip = inputActions.EJXRInput.GripBtn;
        XRButtonTrigger = inputActions.EJXRInput.TriggerBtn;

        XRButtonPrimary.Enable();
        XRButtonSecondary.Enable();
        XRButtonGrip.Enable();
        XRButtonTrigger.Enable();
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        lr = rightHand.GetComponent<LineRenderer>();
        print("lr이 누구의 것이니.." +lr.gameObject);
        lr.enabled = false;

        interactivePropsLayer = 1 << LayerMask.NameToLayer("interactiveProps");
    }

    // Update is called once per frame
    void Update()
    {
        if (XRButtonTrigger.triggered)
        {
            lr.enabled = true;
            ClickByRay();
        }
        //떼면?

        #region keyboard Test

        if (Input.GetMouseButton(0))
        {
            lr.enabled = true;
            ClickByRaytest();
        }else if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false; 
        }
        #endregion
    }

    private void ClickByRaytest()
    {
        Ray ray = new Ray(rightHand.position, rightHand.forward);

        lr.SetPosition(0, ray.origin);
        RaycastHit hit;

        bool isHitInteractive = Physics.Raycast(ray, out hit, 10f, interactivePropsLayer);

        if (isHitInteractive)
        {
            if (hit.transform.gameObject.CompareTag("movable") && Input.GetKeyDown(KeyCode.M))
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }
    }

    void ClickByRay()
    {
        Ray ray = new Ray(rightHand.position, rightHand.forward);

        lr.SetPosition(0, ray.origin);
        RaycastHit hit;

        bool isHitInteractive = Physics.Raycast(ray, out hit, 10f, interactivePropsLayer);

        if (isHitInteractive)
        {
            if (hit.transform.gameObject.CompareTag("movable") && XRButtonPrimary.triggered)
            {
                lr.SetPosition(1, hit.point);              
            }
        }else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }
    }
}

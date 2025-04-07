using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.InputSystem.InputAction;
using Unity.VisualScripting;

public class EJPropGrabLHand : MonoBehaviour
{
    LayerMask interactivePropsLayer;
    Transform hitObject;
    LineRenderer lr;

    bool isMovable;
    bool isGrabbable;
    bool isHighlighted;

    [SerializeField]
    private Sprite[] sprites;

    Image centerPoint;
    Sprite sourceImage;
    RectTransform rectTransform;

    public GameObject RightHand;
    public GameObject LeftHand;

    GameObject grabObject;

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

    public GameObject[] interactiveProps;

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


    // Start is called before the first frame update
    void Start()
    {
        interactivePropsLayer = 1 << LayerMask.NameToLayer("interactiveProps");
    }

    // Update is called once per frame
    void Update()
    {

        return;

        Ray ray = new Ray(LeftHand.transform.position, LeftHand.transform.forward);
        RaycastHit[] hit = Physics.RaycastAll(ray, /*float.MaxValue*/ 10f, interactivePropsLayer);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);

        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                hitObject = hit[i].transform;
                //print("왼손ray에 닿은 물체는" + hit[i].transform.gameObject.name);

                //01.outline
                #region outline
                Outline outline = hitObject.gameObject.GetComponent<Outline>();

                if (hitObject != null && hitObject != hit[i].transform)
                {
                    outline = hitObject.gameObject.GetComponent<Outline>();
                    outline.enabled = false;
                }

                if (outline != null)
                {
                    if (!isHighlighted)
                    {
                        outline.enabled = true;
                        outline.OutlineColor = Color.white;
                        outline.OutlineWidth = 7f;
                        isHighlighted = true;
                    }
                    else
                    {
                        outline.enabled = false;
                        isHighlighted = false;
                    }
                }
                #endregion

                #region grabbable
                //raycastall
                if (hitObject.CompareTag("grabbable"))
                {
                    //centerPoint.sprite = sprites[2];
                    isGrabbable = true;

                    //if (Input.GetButtonDown("Fire1"))
                    if (XRButtonGrip.triggered)
                    {
                        print("grip버튼이 눌렸다");
                        Rigidbody rb = hitObject.gameObject.GetComponent<Rigidbody>();
                        //MeshRenderer mr = hitObject.gameObject.GetComponent<MeshRenderer>();

                        rb.useGravity = false;
                        rb.isKinematic = true;

                        //grabObject = Instantiate(hitObject.gameObject);
                        //mr.enabled = false;
                        //RightHand.transform.parent = grabObject.transform;

                        hitObject.gameObject.transform.parent = LeftHand.transform;
                        hitObject.transform.forward = LeftHand.transform.forward;

                        #region 가져오는 물체들 종류
                        //hitObject.position = LeftHand.transform.position;
                        //hitObject.up = LeftHand.transform.up;

                        if (XRButtonPrimary.triggered)
                        {

                            if (hit[i].transform.name.Contains("dialAxis"))
                            {
                                //interactiveProps[2].GetComponent<EJPropRadioTest>().TurnONOFFradio();
                                StartCoroutine(interactiveProps[2].GetComponent<EJPropRadioTest>().ControlFrequencyByDial(false,true));
                            }
                            else if (hit[i].transform.gameObject.name.Contains("pwBtn"))
                            {
                                interactiveProps[2].GetComponent<EJPropRadioTest>().powerONOFF();
                            }
                            else if (hit[i].transform.gameObject.name.Contains("@FlashLight"))
                            {
                                interactiveProps[3].GetComponent<EJPropFlashlight>().TurnONOFFLight();
                            }
                            else if (hit[i].transform.gameObject.name.Contains("@velve2test"))
                            {
                                StartCoroutine(interactiveProps[4].GetComponent<EJPropGasVelve>().OpenDoor());
                            }
                        }
                    }
                    //if (Input.GetMouseButtonDown(1))
                    if (XRButtonSecondary.triggered)
                    {
                        //붙어있던 애가 떨어져야 한다.

                        interactiveProps[2].GetComponent<EJPropRadioTest>().turnOFFradio();
                    }
                    #endregion
                }

                #endregion
            }
        }
        else
        {
            if (hitObject != null)
            {
                var ol = hitObject.gameObject.GetComponent<Outline>();
                ol.enabled = false;
                isHighlighted = false;
                hitObject = null;

                //centerPoint.sprite = sprites[0];
                isMovable = false;
                isGrabbable = false;
            }
        }
    }
}

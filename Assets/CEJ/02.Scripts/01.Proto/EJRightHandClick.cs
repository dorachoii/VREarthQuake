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


public class EJRightHandClick : MonoBehaviour
{
    LayerMask interactivePropsLayer;
    int outlines;
    Transform hitObject;
    LineRenderer lr;

    bool isMovable;
    bool isGrabbable;
    bool isHighlighted;

    #region Sprites (not used)

    //[SerializeField]
    //private Sprite[] sprites;

    //Image centerPoint;
    //Sprite sourceImage;
    //RectTransform rectTransform;
    #endregion

    public GameObject RightHand;
    public GameObject LeftHand; 


    #region �ϴ� �밡��

    public GameObject[] interactiveProps;

    #endregion

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

    List<string> interProps = new List<string>();
    //Dictionary<string, int> dicInterProps = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        interProps.Add("@door");
        interProps.Add("SM_Door1");
        interProps.Add("SM_Door2");
        interProps.Add("@fusebox");
        interProps.Add("@slippers");

        //dicInterProps["dialAxis"] = 2;
        //dicInterProps["pwBtn"] = 2;
        //dicInterProps["@FlashLight"] = 3;
        //dicInterProps["@velve2test"] = 4;

        outlines = 1 << LayerMask.NameToLayer("interactiveProps") | 1 << LayerMask.NameToLayer("movableProps");
    } 

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(RightHand.transform.position, RightHand.transform.forward);
        RaycastHit[] hit = Physics.RaycastAll(ray, 10f, outlines);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10f);
        
        if (hit.Length > 0)
        {

            for (int i = 0; i < hit.Length; i++)
            {
                hitObject = hit[i].transform;
                print("ray에 닿은 물체는" + hit[i].transform.gameObject.name);

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

                //02.sprites Changes

                //02-1.movable test 완료
                #region movable
                if (hit[i].transform.CompareTag("movable"))
                {                                   
                    //centerPoint.sprite = sprites[1];
                    isMovable = true;

                    #region Prior Click
                    if (XRButtonPrimary.triggered)
                    {
                        if (hit[i].transform.gameObject.name.Contains("@door"))
                        {
                            StartCoroutine(interactiveProps[0].GetComponent<EJDoorOpen1>().OpenDoor());
                            //interactiveProps[0].GetComponent<EJDoorOpen1>().OpenDoor1();
                        }
                        else if (hit[i].transform.gameObject.name.Contains("SM_Door1"))
                        {
                            StartCoroutine(interactiveProps[0].GetComponent<EJDoorOpen2_frontDoor>().OpenDoor());
                            //interactiveProps[0].GetComponent<EJDoorOpen2>().OpenDoor2();
                        }
                        else if (hit[i].transform.gameObject.name.Contains("SM_Door2"))
                        {
                            StartCoroutine(interactiveProps[0].GetComponent<EJDoorOpen3>().OpenDoor());
                            //interactiveProps[0].GetComponent<EJDoorOpen3>().OpenDoor3();
                        }
                        else if (hit[i].transform.gameObject.name == "@fusebox")
                        {
                            StartCoroutine(interactiveProps[1].GetComponent<EJPropFusebox>().OpenDoor());
                        }
                        else if (hit[i].transform.gameObject.name == "@slippers")
                        {
                            interactiveProps[5].GetComponent<EJPropSlippers>().WearShoes();
                        }
                        else if (hit[i].transform.gameObject.name.Contains("radio"))
                        {
                            print("ray에 닿은 물체가 radio를 가지고 있다.");
                            interactiveProps[2].GetComponent<EJPropRadioTest>().powerONOFF();                          
                        }
                        else if (hit[i].transform.gameObject.name.Contains("@FlashLight"))
                        {
                            interactiveProps[3].GetComponent<EJPropBase>().DoAction();
                            interactiveProps[3].GetComponent<EJPropFlashlight>().TurnONOFFLight();
                        }
                        else if (hit[i].transform.gameObject.name.Contains("@velveTrigger"))
                        {
                            StartCoroutine(interactiveProps[4].GetComponent<EJGrabGasvelveTrigger>().OpenDoor());
                        }

                    }
                    #endregion

                }
                #endregion

                #region grabbable
                //raycastall
                else if (hit[i].transform.CompareTag("grabbable"))
                {
                    hit[i].transform.position = LeftHand.transform.position;
                    hit[i].transform.up = LeftHand.transform.up;

                    //centerPoint.sprite = sprites[2];
                    isGrabbable = true;

                    //if (Input.GetButtonDown("Fire1"))
                    if (XRButtonTrigger.triggered)
                    {
                        //for (int j = 0; j < interProps.Count; j++)
                        //{
                        //    if (hit[i].transform.name.Contains(interProps[j]))
                        //    {
                        //        if (XRButtonPrimary.triggered)
                        //        {
                        //            interactiveProps[j].GetComponent<EJPropBase>().DoAction();
                        //            break;
                        //        }
                        //    }
                        //}

                        //if(dicInterProps.ContainsKey(hit[i].transform.name))
                        //{
                        //    interactiveProps[dicInterProps[hit[i].transform.name]].GetComponent<EJPropBase>().DoAction();
                        //}


                        if (hit[i].transform.name.Contains("dialAxis"))
                        {
                            //interactiveProps[2].GetComponent<EJPropRadioTest>().TurnONOFFradio();
                            StartCoroutine(interactiveProps[2].GetComponent<EJPropRadioTest>().ControlFrequencyByDial(false, false));
                        }
                        else if (hit[i].transform.gameObject.name.Contains("pwBtn"))
                        {
                            interactiveProps[2].GetComponent<EJPropRadioTest>().powerONOFF();
                            //interactiveProps[2].GetComponent<EJPropBase>().DoAction();
                        }
                        //else if (hit[i].transform.gameObject.name.Contains("@FlashLight"))
                        //{
                        //    interactiveProps[3].GetComponent<EJPropFlashlight>().TurnONOFFLight();
                        //    //interactiveProps[3].GetComponent<EJPropBase>().DoAction();
                        //}
                        else if (hit[i].transform.gameObject.name.Contains("@velve2test"))
                        {
                            StartCoroutine(interactiveProps[4].GetComponent<EJPropGasVelve>().OpenDoor());
                        }
                    }
                    //if (Input.GetMouseButtonDown(1))
                    if (XRButtonSecondary.triggered)
                    {
                        interactiveProps[2].GetComponent<EJPropRadioTest>().turnOFFradio();
                    }
                }

                #endregion

                #region ipad
                //else if (hit[i].transform.comparetag("numbtn"))
                //{
                //    print("부딪힌 numbtn은" + hit[i].transform.gameobject.name);
                //    button btn = hit[i].transform.getcomponent<button>();

                //    //if (input.getbuttondown("fire1"))
                //    if (xrbuttonprimary.triggered)
                //    {
                //        btn.onclick.invoke();
                //    }
                //}
                //else
                //{
                //    //centerpoint.sprite = sprites[0];
                //    ismovable = false;
                //    isgrabbable = false;
                //}
                #endregion
            }
        }    //Raycast = null�̶��
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

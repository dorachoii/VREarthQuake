using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.InputSystem.InputAction;

public class EJGrabGasvelveTrigger : MonoBehaviour
{
    MeshRenderer velve2mesh;

    public Transform Hand;
    public Transform tempHand1;
    public Transform tempHand2;

    bool bRotate;

    float minAngle = -32;
    float maxAngle = 32;

    AudioSource audiosource;
    AudioClip velveSFX;

    public GameObject velve2;

    #region DoorRot Variables

    Quaternion startQ;
    Quaternion endQ;

    Vector3 startRot;
    Vector3 endRot;


    float rotSpeed = 2f;

    bool isOpen;
    #endregion

    #region VRInput Variables

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
        XRButtonPrimary = inputActions.EJXRInput.PrimaryBtn;
        XRButtonSecondary = inputActions.EJXRInput.SecondaryBtn;
        XRButtonGrip = inputActions.EJXRInput.GripBtn;
        XRButtonTrigger = inputActions.EJXRInput.TriggerBtn;


        XRButtonPrimary.Enable();
        XRButtonSecondary.Enable();
        XRButtonGrip.Enable();
        XRButtonTrigger.Enable();
    }

    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        velve2mesh = GetComponent<MeshRenderer>();

        audiosource = GetComponent<AudioSource>();
        velveSFX = GetComponent<AudioSource>().clip;


        startRot = new Vector3(0, 0, 25);
        startQ = Quaternion.Euler(startRot);

        endRot = new Vector3(0, 0, -25);
        endQ = Quaternion.Euler(endRot);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("velveTrigger에 닿은 물체의 이름은" + other.gameObject);

        if (other.gameObject.CompareTag("Player") /*&& XRButtonPrimary.triggered*/)
        {
            //print("velve2 + primary triggered");
            velve2.GetComponent<MeshRenderer>().enabled = false;
            velve2.GetComponent<Collider>().enabled = false;

            ONMeshvalveTrigger();
            transform.position = origin;
            //velve2mesh.enabled = true;
            //GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha7))
        {
            //ONMeshvalveTrigger();
            //transform.Rotate(0, 0, 30);
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        if(XRButtonPrimary.triggered)
        {
            bRotate = true;
            playVelveSFX();
            tempHand1.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);
        };

        if (bRotate)
        {
            tempHand2.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);

            float angle = Vector3.Angle(tempHand1.up, tempHand2.up);

            int dir = -1;

            if (Vector3.Dot(tempHand2.up, tempHand1.right) > 0)
            {
                dir = 1;
            }

            transform.Rotate(0, 0, angle * dir);

            tempHand1.localEulerAngles = tempHand2.localEulerAngles;
        }

        if(XRButtonSecondary.triggered)
        {
            bRotate = false;
            playVelveSFX();
        };

        //angle limit
        //Vector3 rot = transform.rotation.eulerAngles;
        //rot.z = Mathf.Clamp(rot.z, minAngle, maxAngle);
        //transform.rotation = Quaternion.Euler(rot);
    }

    public void ONMeshvalveTrigger()
    {
        print("onmeshvalve");
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void playVelveSFX()
    {
        audiosource.PlayOneShot(velveSFX);
    }

    public IEnumerator OpenDoor(/*GameObject door1*/)
    {
        playVelveSFX();
        if (!isOpen)
        {

            while (true)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(transform.localRotation, endQ) < 0.5f)
                {
                    transform.localRotation = endQ;
                    isOpen = true;
                    EJToDoManager.instance.ONcheckGasValve();

                    PJH_ShakeManager.instance.VelveShock();
                    PJH_Notifiction.instance.AfterShock();


                    yield break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(transform.localRotation, startQ) < 0.5f)
                {
                    transform.localRotation = startQ;
                    isOpen = false;
                    
                    yield break;
                }
                yield return null;
            }
        }
    }

}

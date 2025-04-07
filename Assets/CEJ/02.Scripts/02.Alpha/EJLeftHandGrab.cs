using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

// grib을 누르면 그 물체가 내 손의 자식 오브젝트로 들어온다.
// 손이 그 물체의 중력의 영향을 받아 떨어지면 안되니까 rigidbody를 끈다
// 가져온 물체의 forward방향을 손의 forward방향으로 맞춰준다.

// grib을 한 번 더 누르면 rigidbody를 켜고
// 부모 오브젝트를 null로 만들어서 그 자리에서 떨어지게 한다.

public class EJLeftHandGrab : MonoBehaviour
{
    //public GameObject LHand;
    public GameObject LHand;
    LineRenderer Lline;
    LineRenderer RLine;

    public GameObject grabPocket;
    Vector3 originSize;

    public GameObject valveTrigger;

    LayerMask movableProps;
    int grabObj;

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
        RLine = LHand.GetComponent<LineRenderer>();
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

        XRButtonGrip.started += OnStarted;
        XRButtonGrip.canceled += OnCanceled;
    }
    private void OnDisable()
    {
        XRButtonGrip.started -= OnStarted;
        XRButtonGrip.canceled -= OnCanceled;
    }
    enum HandState
    {
        None,
        Pressed,
        Pressing,
        Released,
    }

    HandState handState;
    void OnStarted(CallbackContext context)
    {
        RLine.enabled = true;
        handState = HandState.Pressed;
    }

    void OnCanceled(CallbackContext context)
    {
        RLine.enabled = false;
        handState = HandState.Released;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Lline = LHand.GetComponent<LineRenderer>();
        movableProps = 1 << LayerMask.NameToLayer("interactiveProps");
        grabObj = 1 << LayerMask.NameToLayer("movableProps") | 1 << LayerMask.NameToLayer("obstacles");
    }

    

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(LHand.transform.position, LHand.transform.forward);
        RaycastHit hit;

        ClickByRay();
    }

    bool isGrabbed;
    GameObject grabObject;

    private void ClickByRay()
    {
        Ray ray = new Ray(LHand.transform.position, LHand.transform.forward);
        RaycastHit hit;

        // 이동은 Lerp로 부드럽게 손으로 가져와진다.
        if (handState == HandState.Pressing && grabObject)
        {
            //grabObject.transform.localScale = originSize;
            grabObject.transform.position = Vector3.Lerp(grabObject.transform.position, LHand.transform.position + LHand.transform.forward*1.1f, Time.deltaTime * 10);

        }

        if (Physics.Raycast(ray, out hit, 10f, /*interactivePropsLayer*/grabObj))
        {
            //print("hit에 닿은 것은" + hit.transform.gameObject.name);

            if (handState == HandState.Pressed && !isGrabbed)
            {
                if (hit.transform.gameObject.name.Contains("ipad"))
                {
                    EJToDoManager.instance.ONcheckTablet();
                    
                }


                // grib을 누르면 그 물체가 내 손의 자식 오브젝트로 들어온다.
                hit.transform.gameObject.transform.parent = grabPocket.transform;
                //hit.transform.localPosition = Vector3.one;
                //hit.transform.localScale = Vector3.one * 0.1f;
                print("hit의 parent는" + hit.transform.gameObject.transform.parent);

                isGrabbed = true;

                // 손이 그 물체의 중력의 영향을 받아 떨어지면 안되니까 rigidbody를 끈다
                if (null != hit.transform.gameObject.GetComponent<Rigidbody>())
                {
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }


                // 가져온 물체의 forward방향을 손의 forward방향? mainCam의 forward 방향으로 맞춰준다.
                hit.transform.forward = - Camera.main.transform.forward;
                grabObject = hit.transform.gameObject;

                if (hit.transform.gameObject.name.Contains("velve2")&&XRButtonPrimary.triggered)
                {
                    print("valve22222");
     
                        valveTrigger.GetComponent<EJGrabGasvelveTrigger>().ONMeshvalveTrigger();
                    
                }
          
            }
        }
        else
        {
            // 허공
        }

        if (handState == HandState.Released)
        {
            if (null != grabObject)
            {
                // grib을 한 번 더 누르면 rigidbody를 켜고
                //LHand.transform.gameObject.GetComponent<MeshCollider>().enabled = true;
                if (null != grabObject.transform.gameObject.GetComponent<Rigidbody>())
                {
                    grabObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    grabObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                //튕기는 걸로 해야함


                // 부모 오브젝트를 null로 만들어서 그 자리에서 떨어지게 한다.
                grabObject.transform.gameObject.transform.parent = null;
                //grabObject.transform.localScale *= 10; /** 10f*/ //물체별 가중치 필요;

                isGrabbed = false;
                grabObject = null;

                //LHand.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
        }


        if (handState == HandState.Pressed)
            handState = HandState.Pressing;
        else if (handState == HandState.Released)
            handState = HandState.None;
    }
}

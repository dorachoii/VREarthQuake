using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EJXRToolkitInput : MonoBehaviour
{
    @XRIDefaultInputActions inputActions = new XRIDefaultInputActions();
    InputAction actionMove;
    // Start is called before the first frame update
    void Start()
    {
        actionMove = inputActions.XRIRightHandLocomotion.Move;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axis = actionMove.ReadValue<Vector2>();
        if (axis != Vector2.zero)
        {
            print(axis);

        }
    }
}

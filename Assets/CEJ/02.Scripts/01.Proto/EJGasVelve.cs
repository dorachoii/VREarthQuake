using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJGasVelve : MonoBehaviour
{
    public Transform Hand;
    public Transform tempHand1;
    public Transform tempHand2;

    bool bRotate;

    Vector3 rz;

    // Start is called before the first frame update
    void Start()
    {
        rz = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            bRotate = true;
            tempHand1.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);
        }

        if (bRotate)
        {
            tempHand2.localEulerAngles = new Vector3(0, 0, Hand.localEulerAngles.z);

            float angle = Vector3.Angle(tempHand1.up, tempHand2.up);

            int dir = 1;

            if (Vector3.Dot(tempHand2.up, tempHand1.right) > 0)
            {
                dir = -1;
            }

            rz.z += angle * dir;
            rz.z = Mathf.Clamp(rz.z , -35, 35);

            transform.localEulerAngles = rz;
            //transform.Rotate(0, 0, rz);

            tempHand1.localEulerAngles = tempHand2.localEulerAngles;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            bRotate = false; 
        }
    }
}

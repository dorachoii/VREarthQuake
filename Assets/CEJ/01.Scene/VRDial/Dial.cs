using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public Transform hand;
    public Transform tempHand1;
    public Transform tempHand2;

    bool bRotate;

    void Start()
    {

    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bRotate = true;
            tempHand1.localEulerAngles = new Vector3(0, 0, hand.localEulerAngles.z);
        }

        if(bRotate)
        {
            tempHand2.localEulerAngles = new Vector3(0, 0, hand.localEulerAngles.z);

            //다이얼이 회전해야하는 각도
            float angle = Vector3.Angle(tempHand1.up, tempHand2.up);
            print(angle);

            //방향
            int dir = 1;
            //오른쪽으로 돌렸다
            //if(Vector3.Angle(tempHand2.up, tempHand1.right) < 90)
            if(Vector3.Dot(tempHand2.up, tempHand1.right) > 0)
            {
                dir = -1;
            }

            transform.Rotate(0, 0, angle * dir);

            tempHand1.localEulerAngles = tempHand2.localEulerAngles;
        }
    }
}

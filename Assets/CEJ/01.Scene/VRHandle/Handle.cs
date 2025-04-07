using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Transform hand;
    public Transform insideHand;
    public Transform handleModel;

    bool isHold;

    // 회전한 누적 값
    public float rotateAngle;
    // 돌아갈 때 회전해야하는 방향
    int dir = 0;

    Vector3 toHand;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            isHold = true;

            SetInsideHand();

            toHand = insideHand.localPosition.normalized;

            //원상복구 하는 도중 남은 각도를 원래값으로 되돌리자 
            rotateAngle *= -dir;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            isHold = false;

            //핸들을 되돌리는 방향을 건들자
            
            if (rotateAngle > 0) dir = -1;
            else dir = 1;
            
            rotateAngle = Mathf.Abs(rotateAngle);
        }

        if(isHold)
        {
            SetInsideHand();

            RotateHandle();

            // 실제 핸들의 모양의 각도만큼 기울이자 
            insideHand.RotateAround(transform.position, transform.right, 25);
        }

        else
        {
	        if(rotateAngle != 0)
            {
                handleModel.Rotate(0, 0, 100 * dir * Time.deltaTime);
                rotateAngle -= 100 * Time.deltaTime;
                if(rotateAngle <= 0)
                {
                    rotateAngle = 0;
                    handleModel.localEulerAngles = new Vector3(25, 0, 0);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            insideHand.localEulerAngles = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //원래 손의 위치로 맞추자 
            insideHand.position = hand.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Vector3 localPos = insideHand.localPosition;
            //z 값을 없애서 핸들과 같은 라인에 위치시키고 핸들의 크기만큼 만들어주자 
            localPos.z = 0;
            insideHand.localPosition = localPos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Vector3 localPos = insideHand.localPosition;
            //z 값을 없애서 핸들과 같은 라인에 위치시키고 핸들의 크기만큼 만들어주자 
            localPos = localPos.normalized * 0.5f;
            insideHand.localPosition = localPos;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            // 실제 핸들의 모양의 각도만큼 기울이자 
            insideHand.RotateAround(transform.position, transform.right, 25);
        }
    }

    void SetInsideHand()
    {
        //수직으로 세우자 
        insideHand.localEulerAngles = Vector3.zero;

        //원래 손의 위치로 맞추자 
        insideHand.position = hand.position;
        Vector3 localPos = insideHand.localPosition;
        //z 값을 없애서 핸들과 같은 라인에 위치시키고 핸들의 크기만큼 만들어주자 
        localPos.z = 0;
        localPos = localPos.normalized * 0.5f;
        insideHand.localPosition = localPos;
    }

    void RotateHandle()
    {
        //이동한 손
        //insideHand.localPosition.normalized;
        //이전 손
        //toHand
        //왼쪽으로 돌리면 외적한 값이 Vector3.back
        //오른쪽으로 돌리면 외적한 값이 Vector3.forward
        Vector3 v = Vector3.Cross(toHand, insideHand.localPosition.normalized);
        print(v.normalized + "," + Vector3.forward);

        //toHand, insideHand.localPosition.normalized 각도 핸들을 만큼 회전 시키자
        //v.normalize.z 는 angle 을 -냐 +냐 할지 정하기 위해 
        float angle = Vector3.Angle(toHand, insideHand.localPosition.normalized) * v.normalized.z;
        rotateAngle += angle;
        handleModel.Rotate(0, 0, angle);

        //toHand 를 갱신하자 
        toHand = insideHand.localPosition.normalized;
    }
}

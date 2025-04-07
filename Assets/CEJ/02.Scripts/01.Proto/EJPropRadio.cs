using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class EJPropRadio : MonoBehaviour
{
    public Transform hand;
    public Transform tempHand1;
    public Transform tempHand2;

    #region dial Variables

    public GameObject dialAxis;
    public GameObject gridLine;

    float gridLinePosX;
    float gridLineStartPosX = 0;
    float gridLineEndPosX = -0.05f;

    bool bRotate;

    Quaternion startQ;
    Quaternion endQ;
    Vector3 startRot;
    Vector3 endRot;

    float rotSpeed = 1f;

    bool isOpen;

    #endregion

    #region SFX
    public AudioSource noise;
    public AudioSource broadCast;
    float noiseVolume;
    float broadcastVolume;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        noiseVolume = noise.volume;
        broadcastVolume = broadCast.volume;

        //임의 각도
        startRot = new Vector3(0, 0, 0);
        startQ = Quaternion.Euler(startRot);
        endRot = new Vector3(0, 0, 90);
        endQ = Quaternion.Euler(endRot);

        
    }

    // Update is called once per frame
    void Update()
    {
        //float angle = Vector3.Angle(Vector3.up, dialAxis.transform.up);
        //angle = Mathf.Clamp(angle, 0, 180);

        //noiseVolume = 1 - (angle / noiseVolume);
        //broadcastVolume = angle / broadcastVolume;

        #region grabbable Rotation시 바꿀 코드
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    bRotate = true;
        //    tempHand1.localEulerAngles = new Vector3(0, 0,hand.transform.localEulerAngles.z);
        //}

        //if (bRotate)
        //{
        //    //angle between temp1, temp2
        //    tempHand2.localEulerAngles = new Vector3(0, 0, hand.localEulerAngles.z);

        //    float angle = Vector3.Angle(tempHand1.up, tempHand2.up);
        //    print("RadioDialAngle은" + angle);

        //    //direction
        //    int dir = 1;

        //    //오른쪽으로 돌리면 사이각이 90도 이하일 수 밖에 없다.
        //    //즉, 내적값이 양수
        //    if (Vector3.Dot(tempHand2.up, tempHand1.right) > 0)
        //    {
        //        dir = -1;
        //    }

        //    dialAxis.transform.Rotate(0, 0, angle * dir);

        //    //돌리고 나서 temp1과 temp2를 같게 하면 더 이상 회전하지 않음
        //    tempHand1.localEulerAngles = tempHand2.localEulerAngles;
        //}
        #endregion

        #region 라디오
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    print("dial을 돌리기 전 rotation값은" + dialAxis.transform.localRotation);
        //    print("dial을 돌리기 전 noise값은" + noiseVolume);
        //    print("dial을 돌리기 전 broadcast값은" + broadcastVolume);
        //    print("dial을 돌리기 전 gridLine의 positionX 값은" + gridLinePosX);

        //    DialRot(dialAxis);

        //    print("dial을 돌린 후의 rotation값은" + dialAxis.transform.localRotation);
        //    print("dial을 돌린 후의 noise값은" + noiseVolume);
        //    print("dial을 돌린 후의 broadcast값은" + broadcastVolume);
        //    print("dial을 돌린 후의 gridLine의 positionX 값은" + gridLinePosX);
        //}
        #endregion

    }

    public IEnumerator DialRot(GameObject dial)
    {
        gridLinePosX = gridLine.transform.position.x;
        noise.Play();
        broadCast.Play();

        if (!isOpen)
        {
            while (true)
            {
                dial.transform.localRotation = Quaternion.Lerp(dial.transform.localRotation, endQ, Time.deltaTime * rotSpeed);
                

                gridLinePosX = Mathf.Lerp(gridLinePosX, gridLineEndPosX, Time.deltaTime * rotSpeed);

                noiseVolume = Mathf.Lerp(1, 0, Time.deltaTime * rotSpeed);
                broadcastVolume = Mathf.Lerp(0, 1, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(dial.transform.localRotation, endQ) < 0.5f)
                {
                    dial.transform.localRotation = endQ;
                    isOpen = true;

                    noiseVolume = 0;
                    broadcastVolume = 1;
                    break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                dial.transform.localRotation = Quaternion.Lerp(dial.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                gridLinePosX = Mathf.Lerp(gridLinePosX, gridLineStartPosX, Time.deltaTime * rotSpeed);

                noiseVolume = Mathf.Lerp(0, 1, Time.deltaTime * rotSpeed);
                broadcastVolume = Mathf.Lerp(1, 0, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(dial.transform.localRotation, startQ) < 0.5f)
                {
                    dial.transform.localRotation = startQ;
                    isOpen = false;

                    noiseVolume = 1;
                    broadcastVolume = 2;

                    break;
                }
                yield return null;
            }
        }
    }
}
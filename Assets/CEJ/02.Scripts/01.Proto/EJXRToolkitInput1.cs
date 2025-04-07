using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class EJXRToolkit_InputTest : MonoBehaviour
{
    //public InputActionProperty testAction;
    //public InputActionProperty testAction2;

    ////left를 받아서 인스펙터 창에서 미리 맵핑되어 있는 KeyCode의 이름에 접근해서 사용하고 싶다.
    //public ActionBasedController leftControl;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //이거 위치가 start가 맞나?
    //    var inputDevices = new List<UnityEngine.XR.InputDevice>();
    //    UnityEngine.XR.InputDevices.GetDevices(inputDevices);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //vr연동하고 눌렀을 때 나오는지 확인 필요
    //    if(leftControl.selectAction.action.triggered)
    //    {
    //        print("왼쪽 트리거 버튼 클릭");
    //    }

    //    //testAction으로 접근할 시
    //    if (testAction.action.triggered)
    //    {
    //        print("왼쪽 눌렸다");
    //    }
    //    if (testAction2.action.triggered)
    //    {
    //        print("오른쪽 눌렸다");
    //    }



    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        print("왼손의 Y버튼이 눌렸다");
    //    }

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        print("왼손의 X버튼이 눌렸다");
    //    }

    //}
}

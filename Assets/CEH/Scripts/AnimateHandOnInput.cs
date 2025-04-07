using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class AnimateHandOnInput : MonoBehaviour
{
    // コントローラーの回転入力
    public InputActionProperty controllerRotationInput;
    // ボタンの入力
    public InputActionProperty triggerInput;
    // 手のアニメーター
    public Animator handAnimator;

    // 回転の比較用Quaternion
    private Quaternion initialRotation;
    private Quaternion currentRotation;
    private Quaternion currentControllerRotation;
    private bool bRotate = false;

    // 動かすオブジェクト
    public Transform dialTransform;
    public GameObject targetBall;

    void Update()
    {
        // アニメーション用
        float triggerValue = triggerInput.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        // 現在のコントローラーのQuaternion
        currentControllerRotation = controllerRotationInput.action.ReadValue<Quaternion>();

        // ボタンが押された瞬間の回転
        if (triggerInput.action.IsPressed())
        {
            if (!bRotate)
            {
                bRotate = true;
                initialRotation = controllerRotationInput.action.ReadValue<Quaternion>();
            }
        }

        // 回転中
        if (bRotate)
        {
            currentRotation = currentControllerRotation;

            // 回転前後のUpベクトル間の角度を計算
            float angle = Vector3.Angle(initialRotation * Vector3.up, currentRotation * Vector3.up);
            int direction = -1;

            // 内積を用いて回転方向を判定
            if (Vector3.Dot(currentRotation * Vector3.up, initialRotation * Vector3.right) > 0)
            {
                direction = 1;
            }

            // ダイヤルを回転
            dialTransform.Rotate(0, 0, angle * direction * 0.005f);

            // ボールの移動
            float ballMovementSpeed = angle * direction * 0.5f;
            Vector3 moveDir = ballMovementSpeed * Vector3.left;
            Vector3 newBallPos = targetBall.transform.position + moveDir * Time.deltaTime;

            // ボールのx位置を制限
            newBallPos.x = Mathf.Clamp(newBallPos.x, -7f, 14f);
            targetBall.transform.position = newBallPos;
        }

        // ボタンが離されたら回転を終了
        if (triggerValue == 0)
        {
            bRotate = false;
        }
    }
}

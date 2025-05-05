
![hand3](https://github.com/user-attachments/assets/0f60206c-a1b9-459b-b2ae-2ea6562b1bd8)


## 🧑‍🎤 一言紹介

脱出ゲーム形式を取り入れ、地震発生時の避難方法を教えるVR体験。

このゲームは**Oculus MetaQuest 2**向けであり、**XR Interaction ToolKit**で開発しました。
<br>
<br>

## 📺 プレイ動画
[https://www.youtube.com/shorts/xhghoX2gcs4](https://www.youtube.com/watch?v=293QAO0pplU)
<br>
<br>

## 🛠 担当部分

- **地震揺れの実装**
- **アイテムの操作インタラクション**
    - Click
    - Grab
    - Grab & Rotate
<br>
<br>

## 🦦 アピールポイントと挑戦課題

<details>
<summary>① XR Toolkit InputActions カスタマイズ</summary>




### 🔧 実装概要

ゲームの操作は以下のように構成しました。XR Toolkit のデフォルトの Input System ではなく、InputActionを定義してバインドする形で、よりシンプルに扱えるようカスタマイズしました。



### 💻 ソースコード

```csharp
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CustomXRInputHandler : MonoBehaviour
{
    // Input System アクションマッピング
    public XRIDefaultInputActions inputActions;
    public InputAction XRButtonPrimary;
    public InputAction XRButtonTrigger;
    public InputAction XRButtonGrip;

    // 手の動作状態を定義
    private enum HandState
    {
        None,
        Pressed,
        Pressing,
        Released,
    }

    private HandState handState;

    // 手から出るレイ用の LineRenderer
    public LineRenderer RLine;

    // Input System のインスタンス
    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
    }

    // イベントを登録
    private void OnEnable()
    {
        XRButtonPrimary = inputActions.EJXRInput.PrimaryBtn;
        XRButtonGrip = inputActions.EJXRInput.GripBtn;
        XRButtonTrigger = inputActions.EJXRInput.TriggerBtn;

        XRButtonPrimary.Enable();
        XRButtonGrip.Enable();
        XRButtonTrigger.Enable();

        XRButtonGrip.started += OnGripStarted;
        XRButtonGrip.canceled += OnGripCanceled;
    }

    // イベントの登録を解除
    private void OnDisable()
    {
        XRButtonGrip.started -= OnGripStarted;
        XRButtonGrip.canceled -= OnGripCanceled;
    }

    // Gripボタンを押した時にレイを表示
    private void OnGripStarted(CallbackContext context)
    {
        RLine.enabled = true;
        handState = HandState.Pressed;
    }

    // Gripボタンを離した時にレイを非表示
    private void OnGripCanceled(CallbackContext context)
    {
        RLine.enabled = false;
        handState = HandState.Released;
    }
}

```
| コントローラー | 作名      | 操作        | 機能例                        |
|----------------|-----------|-------------|-------------------------------|
| 右手           | A         | PrimaryBtn  | Click  | ドア、スイッチ、靴 などを押す     |
| 右手           | Triggger  | TriggerBtn  | Rotate | バルブ・ダイヤル などを回す       |
| 左手           | Grip      | GripBtn     | Grab   | 懐中電灯・タブレットなどを掴む／離す |

</details>

<details>
<summary>② Vector3.Dot()を使った回転方向判定</summary>




### 🔧 実装概要
`Vector3.Dot` で、現在の回転が時計回りか反時計回りか判定しています。

- 時計回り：周波数 → 右へ
- 反時計回り：周波数 → 左へ


### 💻 ソースコード
```csharp
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
```
</details>

<details>
<summary>勉強内容の整理</summary>
![image (6)](https://github.com/user-attachments/assets/feb380dc-8cb1-4174-8b4e-2a217299ccb9)
![image (7)](https://github.com/user-attachments/assets/e4746c24-95c8-42f3-b429-c80e32e5a2c0)

</details>
<br>
<br>

## 🔎 振り返り

### 学んだこと
- 初めてPCやモバイル以外の入力装置を使った開発でした。VR環境でのインタラクションを工夫してみることができました。
- ドアや漏電遮断器など、同じ回転が適用される要素が多かったため、回転コードの再利用性を高めました。
- ベクトルの内積を理解し、それを活用して回転コードを書きました。
<br>

### 改善点
- プレイヤーの身長を1.8mに設定してVR空間を構成しました。見た目には問題なさそうでしたが、一部の通路が非常に狭かったり、アイテムが小さすぎて掴みにくい問題がありました。
- VR環境でのUI設計も難しかったです。固定する必要があるUIと、インタラクションによって生成されるポップアップが重なり、操作しづらい場面がありました。 


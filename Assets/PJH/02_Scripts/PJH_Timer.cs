using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PJH_Timer : MonoBehaviour
{
    

    // 타이머 텍스트 유아이
    public GameObject timeText;
    TextMeshProUGUI timer;

    // 타이머 활성 상태
    bool isTimerStart;

    // 시간 표시
    public Text[] textTime;

    // 시간
    float maxTime;
    [SerializeField] float time;
    int min;
    int sec;


    

    private void Awake()
    {
        // 타이머 컴포넌트 가져오기
        timer = timeText.GetComponent<TextMeshProUGUI>();
        timer.text = "05 : 00";

        time = 301f;
        maxTime = time;

     }
    // Start is called before the first frame update
    void Start()
    {
        isTimerStart = true;

    } 

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    // 타이머 메서드
    void Timer()
    {
        if (isTimerStart == true)
        {
            // 시간이 흘러감
            time -= Time.deltaTime;

            min = (int)(time / 60); // 분
            sec = ((int)(time % 60)); // 초

            timer.text = string.Format("{0:D2} : {1:D2}", min, sec);

        }
    }

} 
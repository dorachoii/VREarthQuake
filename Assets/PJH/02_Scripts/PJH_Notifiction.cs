using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PJH_Notifiction : MonoBehaviour
{
    public static PJH_Notifiction instance;

    public Text Notifiction;
    public GameObject NOT;
    public int textTime = 5;
    AudioSource[] arrayAudio;
     

    private void Awake()
    {
        instance = this;
        arrayAudio = NOT.GetComponents<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartMessage();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void UnHideNotifiction()
    {
        NOT.SetActive(true);
    }
    public void HideNotifiction()
    {
        NOT.SetActive(false);
    }

    public void StartMessage()     //지진 시작 메세지 넣어봤습니다. Start()에서 실행하고, 삭제가능
    {
        UnHideNotifiction();
        Notifiction.text = "지금부터 지진시뮬레이션을 시작합니다.";
        arrayAudio[0].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void Glass()             //유리조각
    {
        UnHideNotifiction();
        Notifiction.text = "유리조각 때문에 지나갈 수 없습니다.";
        arrayAudio[1].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void Radio()             //라디오
    {
        UnHideNotifiction();
        Notifiction.text = "라디오를 켜서 지진 대비 행동수칙을 파악하세요.";
        arrayAudio[2].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void RCD()               //누전차단기
    {
        UnHideNotifiction();
        Notifiction.text = "누전차단기를 내려 전기를 차단하세요.";
        arrayAudio[3].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void Exit()               //탈출구 열기
    {
        UnHideNotifiction();
        Notifiction.text = "출입문을 미리 열어, 탈출로를 확보하세요.";
        arrayAudio[4].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void Velve()               //벨브 잠그기
    {
        UnHideNotifiction();
        Notifiction.text = "벨브를 잠가 폭발을 방지하세요.";
        arrayAudio[5].Play();
        Invoke("HideNotifiction", textTime);
    }
    public void AfterShock()               //여진
    {
        UnHideNotifiction();
        Notifiction.text = "여진이 발생하였습니다.";
        arrayAudio[6].Play();
        Invoke("HideNotifiction", textTime);
    }

    public void Tablet()               //태블릿
    {
        UnHideNotifiction();
        Notifiction.text = "태블릿을 열어 피난장소를 확인하세요.";
        arrayAudio[7].Play();
        Invoke("HideNotifiction", textTime);
    }

}

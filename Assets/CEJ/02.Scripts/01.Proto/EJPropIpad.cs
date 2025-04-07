using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EJPropIpad : MonoBehaviour
{
    //Sound Variables
    AudioSource audiosource;
    AudioClip touchSFX;

    //UI Input
    public GameObject screen;
    public GameObject UI_Btn;
    public GameObject Img_Answer;
    public GameObject Img_Error;

    public TMP_InputField inputfield;
    public string originText;

    string answer = "1234";

    float screenResetTime = 5f;

    public GameObject todoManager;
    //PJH_TodoList todoListScript;
    EJToDoManager todolist;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        touchSFX = GetComponent<AudioSource>().clip;

        //safetyImg = ImgSafetyZone.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //InputBtn
    public void TouchBtn(Button btn)
    {
        string text = btn.GetComponentInChildren<TextMeshProUGUI>().text;
        originText = inputfield.text;;
        inputfield.text = originText + text;
        playTouchSFX();
    }

    //DeleteBtn
    public void DeleteText(Button btn)
    {   
        originText= inputfield.text;
        inputfield.text = originText.Remove(originText.Length - 1);
        playTouchSFX();
    }

    //EnterBtn
    public void EnterCheckAnswer(Button btn)
    {
        playTouchSFX();

        if (inputfield.text == answer)
        {
            if (!Img_Answer.activeSelf)
            {
                UI_Btn.SetActive(false);
                Img_Answer.SetActive(true);
                //todoListScript.Exit(true);
                todolist.ONcheckTablet();
}
        }else
        {
            //비밀번호가 틀렸습니다. 띄우기
            if (!Img_Error.activeSelf)
            {
                UI_Btn.SetActive(false);
                Img_Error.SetActive(true);
            }
        }

        StartCoroutine(resetScreen());
        inputfield.text = "";

    }



    //나중에 trigger시 turnONOFFScreen 실행
    public void TurnOnOFFScreen()
    {
        if (!screen.activeSelf)
        {
            screen.SetActive(true);
        }else
        {
            screen.SetActive(false);
        }
    }

    
    public void playTouchSFX()
    {
        audiosource.PlayOneShot(touchSFX);
    }

    public IEnumerator resetScreen()
    {
        yield return new WaitForSeconds(screenResetTime);

        Img_Answer.SetActive(false);
        Img_Error.SetActive(false);
        UI_Btn.SetActive(true);

        yield return null;

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EJDoorOpen2_frontDoor : MonoBehaviour
{
    public GameObject door2;
    public GameObject UI;

    EJRotateBase ejrotatebase;

    AudioSource audiosource;
    AudioClip doorSFX;

    public GameObject todoManager;
    PJH_TodoList todoListScript;

    #region DoorRot Variables

    Quaternion startQ;
    Quaternion endQ;
    Quaternion slightQ;
    Vector3 startRot;
    Vector3 endRot;
    Vector3 slight;

    float rotSpeed = 2f;

    bool isOpen;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ejrotatebase = new EJRotateBase();
        slight = new Vector3(-90, 0, 110);
        slightQ = Quaternion.Euler(slight);

        startRot = new Vector3(-90, 0, 90);
        startQ = Quaternion.Euler(startRot);

        endRot = new Vector3(-90, 0, 180);
        endQ = Quaternion.Euler(endRot);

        audiosource = door2.GetComponent<AudioSource>();
        doorSFX = door2.GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
            //setRot(0);
            //StartCoroutine(OpenDoor(/*door1*/));
            // 살짝 열기
           // StartCoroutine(ejrotatebase.Rot(door2, startRot, slight));
        //}

    }

    

    public IEnumerator OpenDoor(/*GameObject door2*/)
    {
        PlayDoorSFX();
        if (!isOpen)
        {
            while (true)
            {
                door2.transform.localRotation = Quaternion.Lerp(door2.transform.localRotation, slightQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door2.transform.localRotation, slightQ) < 0.5f)
                {
                    door2.transform.localRotation = slightQ;
                    isOpen = true;
                    EJToDoManager.instance.ONcheckDoor();
                    PJH_Notifiction.instance.Velve();
                    yield break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                door2.transform.localRotation = Quaternion.Lerp(door2.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door2.transform.localRotation, endQ) < 0.5f)
                {
                    door2.transform.localRotation = endQ;
                    UI.gameObject.SetActive(true);
                    isOpen = false;
                    
                    yield break;
                }
                yield return null;
            }
        }
    }

    public void OpenDoor2()
    {
        PlayDoorSFX();
        StartCoroutine(OpenDoor());
    }

    //소리 스무스하게 바꾸기
    public void PlayDoorSFX()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(doorSFX);
        }

        print(audiosource);
        print(doorSFX);
        print("doorSFX 실행");
    }
}

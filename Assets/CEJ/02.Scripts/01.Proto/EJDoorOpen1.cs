using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorInfo
{
    public GameObject door;
    Vector3 startRotWide;
    Vector3 startRotNarrow;
    Vector3 endRot;

}

public class EJDoorOpen1 : MonoBehaviour
{
    DoorInfo[] doorInfos = new DoorInfo[3]; 
    //public GameObject[] doors = new GameObject[3];
    public GameObject door1;

    public GameObject todoManager;
    PJH_TodoList todoListScript;

    AudioSource audiosource;
    AudioClip doorSFX;


    #region DoorRot Variables

    Quaternion startQ;
    Quaternion endQ;
    Quaternion endNQ;
    Vector3 startRot;
    Vector3 endRot;
    Vector3 endRotNarrow;

    float rotSpeed = 2f;

    bool isOpen;
    #endregion

    //살짝 열기 (-90,0,110)

        // Start is called before the first frame update
        void Start()
    {
        endRotNarrow = new Vector3(-90, 0, 110);

        startRot = new Vector3(-90, 0, 0);
        startQ = Quaternion.Euler(startRot);

        endRot = new Vector3(-90, 90, 0);
        endQ = Quaternion.Euler(endRot);

        audiosource = door1.GetComponent<AudioSource>();
        doorSFX = door1.GetComponent<AudioSource>().clip;

        //todoListScript = todoManager.GetComponent<PJH_TodoList>();
    }



    // Update is called once per frame
    void Update()
    {
        #region test
        EJRotateBase ejrotatebase = new EJRotateBase();
        Vector3 slight = new Vector3(-90, 0, 110);




        #endregion
    }
    public IEnumerator Rot(GameObject obj, Vector3 startRot, Vector3 endRot)
    {
        Quaternion startQ = Quaternion.Euler(startRot);
        Quaternion endQ = Quaternion.Euler(endRot);

        if (!isOpen)
        {
            while (true)
            {
                obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(obj.transform.localRotation, endQ) < 0.5f)
                {
                    obj.transform.localRotation = endQ;
                    isOpen = true;
                    

                    break;
                }
                yield return null;
            }
        }
        else
        {
            while (true)
            {
                obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(obj.transform.localRotation, startQ) < 0.5f)
                {
                    obj.transform.localRotation = startQ;
                    isOpen = false;
                    break;
                }
                yield return null;
            }
        }
    }

    public void OpenDoor1()
    {
        PlayDoorSFX();
        StartCoroutine(OpenDoor());
    }


    public IEnumerator OpenDoor(/*GameObject door1*/)
    {
        PlayDoorSFX();
        if (!isOpen)
        {
            
            while (true)
            {
                door1.transform.localRotation = Quaternion.Lerp(door1.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door1.transform.localRotation, endQ) < 0.5f)
                {
                    door1.transform.localRotation = endQ;
                    isOpen = true;
                    todoListScript.DoorOpen(true);

                    yield break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                door1.transform.localRotation = Quaternion.Lerp(door1.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door1.transform.localRotation, startQ) < 0.5f)
                {
                    door1.transform.localRotation = startQ;
                    isOpen = false;
                    todoListScript.DoorOpen(false);
                    yield break;
                }
                yield return null;
            }
        }
    }

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
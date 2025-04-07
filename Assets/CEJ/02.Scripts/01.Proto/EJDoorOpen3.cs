using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EJDoorOpen3 : MonoBehaviour
{
    public GameObject door3;

    AudioSource audiosource;
    AudioClip doorSFX;

    Quaternion startQ;
    Quaternion endQ;
    Vector3 startRot;
    Vector3 endRot;

    float rotSpeed = 2f;

    bool isOpen;

    public GameObject todoManager;
    PJH_TodoList todoListScript;

    // Start is called before the first frame update
    void Start()
    {
        startRot = new Vector3(-90, 0, -90);
        startQ = Quaternion.Euler(startRot);

        endRot = new Vector3(-90, -90, -90);
        endQ = Quaternion.Euler(endRot);

        audiosource = door3.GetComponent<AudioSource>();
        doorSFX = door3.GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //setRot(0);
        //    StartCoroutine(OpenDoor(/*door3*/));
        //}
    }

    public void OpenDoor3()
    {
        PlayDoorSFX();
        StartCoroutine(OpenDoor());
    }

    public IEnumerator OpenDoor(/*GameObject door3*/)
    {
        PlayDoorSFX();
        if (!isOpen)
        {
            while (true)
            {
                door3.transform.localRotation = Quaternion.Lerp(door3.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door3.transform.localRotation, endQ) < 0.5f)
                {
                    door3.transform.localRotation = endQ;
                    isOpen = true;
                    todoListScript.DoorOpen(true);
                    break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                door3.transform.localRotation = Quaternion.Lerp(door3.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(door3.transform.localRotation, startQ) < 0.5f)
                {
                    door3.transform.localRotation = startQ;
                    isOpen = false;
                    todoListScript.DoorOpen(false);
                    break;
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
        print("doorSFX ½ÇÇà");
    }
}

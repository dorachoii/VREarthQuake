using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class EJPropFusebox : MonoBehaviour
{
    public GameObject leverAxis;
    public GameObject wholeLight;

    public GameObject todoManager;
    //PJH_TodoList todoListScript;
    

    AudioSource audiosource;
    AudioClip fuseboxSFX;

    #region FuseboxRot Variables

    Quaternion startQ;
    Quaternion endQ;
    Vector3 startRot;
    Vector3 endRot;

    float rotSpeed = 2f;

    bool isOpen;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        startRot = new Vector3(0, 0, 0);
        startQ = Quaternion.Euler(startRot);

        endRot = new Vector3(100, 0, 0);
        endQ = Quaternion.Euler(endRot);

        audiosource = GetComponent<AudioSource>();
        fuseboxSFX = GetComponent<AudioSource>().clip;

        //todoListScript = todoManager.GetComponent<PJH_TodoList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(OpenDoor(/*leverAxis*/));
        }
    }

    public IEnumerator OpenDoor(/*GameObject leverAxis*/)
    {
        playSFX();
        if (!isOpen)
        {
            while (true)
            {
                leverAxis.transform.localRotation = Quaternion.Lerp(leverAxis.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(leverAxis.transform.localRotation, endQ) < 0.5f)
                {
                    leverAxis.transform.localRotation = endQ;
                    //turnOFFelectro();
                    isOpen = true;
                    EJToDoManager.instance.ONcheckFusebox(true);
                    PJH_Notifiction.instance.Exit();
                    //todoListScript.FuseBox(true);
                    
                    break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                leverAxis.transform.localRotation = Quaternion.Lerp(leverAxis.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(leverAxis.transform.localRotation, startQ) < 0.5f)
                {
                    leverAxis.transform.localRotation = startQ;
                    //turnONelectro();
                    isOpen = false;
                    EJToDoManager.instance.ONcheckFusebox(false);
                    //todoListScript.FuseBox(false);
                    break;
                }
                yield return null;
            }
        }
    }

    public void playSFX()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(fuseboxSFX);           
        }
    }

    public void turnOFFelectro()
    {
        if (!wholeLight.activeSelf)
        {
            wholeLight.SetActive(true);
        }
    }

    public void turnONelectro()
    {
        if (wholeLight.activeSelf)
        {
            wholeLight.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJPropGasVelve : MonoBehaviour
{
    public GameObject velve;

    #region VelveRot Variables

    Quaternion startQ;
    Quaternion endQ;
    Vector3 startRot;
    Vector3 endRot;

    float rotSpeed = 2f;

    bool isOpen;

    AudioSource audiosource;
    AudioClip velveSFX;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        startRot = new Vector3(0, 0, 0);
        startQ = Quaternion.Euler(startRot);

       
        endRot = new Vector3(0, 0, -36);
        endQ = Quaternion.Euler(endRot);

        audiosource = GetComponent<AudioSource>();
        velveSFX = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(OpenDoor(/*velve*/));
        }
    }

    public IEnumerator OpenDoor(/*GameObject velve*/)
    {
        playVelveSFX();

        if (!isOpen)
        {
            while (true)
            {
                velve.transform.localRotation = Quaternion.Lerp(velve.transform.localRotation, endQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(velve.transform.localRotation, endQ) < 0.5f)
                {
                    velve.transform.localRotation = endQ;
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
                velve.transform.localRotation = Quaternion.Lerp(velve.transform.localRotation, startQ, Time.deltaTime * rotSpeed);

                if (Quaternion.Angle(velve.transform.localRotation, startQ) < 0.5f)
                {
                    velve.transform.localRotation = startQ;
                    isOpen = false;
                    break;
                }
                yield return null;
            }
        }
    }

    public void playVelveSFX()
    {
        audiosource.PlayOneShot(velveSFX);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PJH_ShakeManager : MonoBehaviour
{
    public static PJH_ShakeManager instance;

    public List<PJH_Shake> shakeList = new List<PJH_Shake>();

    public GameObject shakeCam;
    public GameObject shakeUI;
    public GameObject Light_Change;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PJH_ShakeManager.instance.DoShake();
            PJH_Notifiction.instance.AfterShock();
            //RenderSettings.ambientLight = new Color(106.0f/255, 29.0f / 255, 29.0f / 255);

            StartCoroutine(Warning());
            Invoke("StopWarning", 3);
        }
    }

    public void VelveShock()
    {
        PJH_ShakeManager.instance.DoShake();
        

        StartCoroutine(Warning());
        Invoke("StopWarning", 3);
    }

    void StopWarning()
    {
        StopCoroutine(Warning()); 
    }
    IEnumerator Warning()
    {
        int count = 0;
        bool a = true;
        while (a)
        {
            RenderSettings.ambientLight = new Color(106.0f / 255, 29.0f / 255, 29.0f / 255);
            yield return new WaitForSecondsRealtime(0.5f);
            RenderSettings.ambientLight = new Color(93.0f / 255, 99.0f / 255, 128.0f / 255);
            yield return new WaitForSecondsRealtime(0.5f);
            count++;
            if(count == 4)
            {
            a = false;
                RenderSettings.ambientLight = new Color(28.0f / 255, 30.0f / 255, 67.0f / 255);
            }
        }

    }

    public void AddShakeObject(PJH_Shake shake)
    {
        shakeList.Add(shake);
    }

    public void DoShake()
    {
        Light_Change.SetActive(false);
        for (int i = 0; i < shakeList.Count; i++)
        {
            shakeList[i].Aftershocks();
        }

        shakeUI.GetComponent<PJH_Warning>().AfterShoke();
        shakeCam.GetComponent<PJH_CameraShake>().Aftershocks();

    }
}

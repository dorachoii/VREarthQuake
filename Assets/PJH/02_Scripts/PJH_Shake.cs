using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJH_Shake : MonoBehaviour
{
    public GameObject Object;
    Vector3 objectPos;

    [SerializeField]
    [Range(0.01f, 0.5f)] float shakeRange = 0.1f;
    [SerializeField]
    [Range(1f, 5f)] float shakeTime = 3f;

    public void Shake()
    {
        objectPos = Object.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", shakeTime);
    }
    public void Aftershocks()
    {
        objectPos = Object.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", shakeTime);
    }
    void StartShake()
    {
        float objectPosX = Random.value * shakeRange * 2 - shakeRange;
        float objectPosZ = Random.value * shakeRange * 2 - shakeRange;
       
        Vector3 objectPos = Object.GetComponent<Rigidbody>().position;
        objectPos.x += objectPosX;
        objectPos.z += objectPosZ;
        Object.transform.position = objectPos;

    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        //Object.transform.position = objectPos;
    }


    // Start is called before the first frame update
    void Start()
    {
        Object = this.gameObject;

        PJH_ShakeManager.instance.AddShakeObject(this);
        Invoke("Shake",6);
    }


    // Update is called once per frame
    void Update()
    {

    }
}

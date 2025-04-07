using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJH_ShakeCameraMove : MonoBehaviour
{
    GameObject shakeCamera;
    public GameObject Stop;
    public float speed = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        shakeCamera = this.gameObject;
        //MoveCamera();
        
    }
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.back * Time.deltaTime * 5);
        if (count == 0)
        {
        transform.position = Vector3.MoveTowards(transform.position, Stop.transform.position, speed * Time.deltaTime);
            
        }
        if (shakeCamera.transform.position == Stop.transform.position) {
            count = 1;
        }
        print(count);
        
        

        
    }

    void MoveCamera()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 5);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Stop)
        {
            shakeCamera.transform.position = Stop.transform.position;
        }
    }*/
}

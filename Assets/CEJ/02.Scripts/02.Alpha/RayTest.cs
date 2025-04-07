using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RayTest : MonoBehaviour
{

    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        //lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        lr.SetPosition(0, ray.origin);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);
        lr.SetPosition(1, ray.origin + ray.direction * 100);
    }
}

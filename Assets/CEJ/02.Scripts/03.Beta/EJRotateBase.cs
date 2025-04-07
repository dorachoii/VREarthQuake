using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EJRotateBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isOpen;
    float rotSpeed = 2f;

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

}

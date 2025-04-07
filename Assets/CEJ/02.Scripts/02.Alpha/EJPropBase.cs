using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJPropBase : MonoBehaviour
{
    protected AudioSource audiosource;
    protected AudioClip propClip;


    float rotSpeed = 2f;
    bool isOpen;

    protected virtual void Start()
    {
        audiosource = GetComponent<AudioSource>();
        propClip = GetComponent<AudioSource>().clip;
    }

    void Update()
    {
        
    }

    public virtual void DoAction()
    {
        audiosource.PlayOneShot(propClip);
    }

    
}

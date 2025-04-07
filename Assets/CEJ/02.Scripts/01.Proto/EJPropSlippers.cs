using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJPropSlippers : MonoBehaviour
{
    public GameObject slippersModel;
    AudioSource audiosource;
    AudioClip wearshoesSFX;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        wearshoesSFX = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WearShoes()
    {
        shoesPlay();

        if (!slippersModel.activeSelf)
        {
            slippersModel.SetActive(true);
        }else
        {
            slippersModel.SetActive(false);
            PJH_Notifiction.instance.Radio();
        }
    }

    public void shoesPlay()
    {
        audiosource.PlayOneShot(wearshoesSFX);
    }
}

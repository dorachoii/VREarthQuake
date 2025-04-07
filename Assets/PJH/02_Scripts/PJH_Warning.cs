using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJH_Warning : MonoBehaviour
{
    //public GameObject warning;
    public GameObject warning2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AfterShoke()
    {
        warning2.SetActive(true);
        //StartCoroutine(Warning());
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}

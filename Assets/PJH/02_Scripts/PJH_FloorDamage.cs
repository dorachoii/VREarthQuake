using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJH_FloorDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("11");
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PJH_PlayerHP>().Damage(1);
        }
        Debug.Log("11");   
    }
}

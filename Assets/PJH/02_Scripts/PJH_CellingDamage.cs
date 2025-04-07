using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJH_CellingDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("22");

        collision.gameObject.GetComponent<PJH_PlayerHP>().Damage(2);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("11");
        other.gameObject.GetComponent<PJH_PlayerHP>().Damage(2);
    }
}

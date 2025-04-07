using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJOutline : MonoBehaviour
{
    GameObject rightHand;
    LayerMask interactivePropsLayer;

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.Find("Right Controller");
        interactivePropsLayer = 1 << LayerMask.NameToLayer("interactiveProps");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void outlineON()
    {
        Ray ray = new Ray(rightHand.transform.position, rightHand.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 40, interactivePropsLayer))
        {
            Outline outline = hit.transform.gameObject.GetComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 7f;
        }
    }


}

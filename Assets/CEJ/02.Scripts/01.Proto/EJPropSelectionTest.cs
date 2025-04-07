using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EJPropSelectionTest : MonoBehaviour
{
    //CenterPoint Sprites Variables
    #region centerpoints
    [SerializeField]
    private Sprite[] sprites;

    Image centerPoint;
    Sprite sourceImage;
    RectTransform rectTransform;
    #endregion

    //Outline Variables
    #region outlines

    private Transform highlight;
    private RaycastHit raycastHit;
    LayerMask interactivePropsLayer;

    GameObject[] interactiveProps;

    #endregion

    //bool check
    bool isHighlighted;
    bool isGrabbable;
    bool isMovable;

    public LayerMask aaaa;

    // Start is called before the first frame update
    void Start()
    {
        centerPoint = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        interactivePropsLayer = 1 << LayerMask.NameToLayer("interactiveProps");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);

        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, interactivePropsLayer))
        {
            //print("raycastHit에 닿은 것은" + raycastHit.transform.gameObject.name + "raycastHit의 태그는" + raycastHit.transform.gameObject.tag);        

            #region Drawing outlines

            highlight = raycastHit.transform;
            Outline outline = highlight.gameObject.GetComponent<Outline>();

            //print("highlight에 닿은 것은" + highlight.gameObject.name + "highlight의 태그는" + highlight.gameObject.tag);        
            if (highlight != null && highlight != raycastHit.transform)
            {
                outline = highlight.gameObject.GetComponent<Outline>();
                outline.enabled = false;
            }

            if (outline != null)
            {
                if (!isHighlighted)
                {
                    outline.enabled = true;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 7f;
                    isHighlighted = true;

                }
                else
                {
                    outline.enabled = false;
                    isHighlighted = false;
                }
            }
            #endregion

            #region CenterUI Changes

            //Default->sprite[0]
            //tag: movable->sprite[1]
            //tag: grabbable->sprite[2]
            if (raycastHit.transform.CompareTag("movable"))
            {
                centerPoint.sprite = sprites[1];
                isMovable = true;
            }
            else if (raycastHit.transform.CompareTag("grabbable"))
            {
                centerPoint.sprite = sprites[2];
                isGrabbable = true;
            }
            else
            {
                centerPoint.sprite = sprites[0];
                isMovable = false;
                isGrabbable = false;
            }
            #endregion

            #region propAction

            ////Door
            //if (highlight.gameObject.CompareTag("Door"))
            //{
            //    print("부딪힌 것은 문입니다");

            //    if (Input.GetKeyDown(KeyCode.Alpha1))
            //    {
            //        print("문이 열립니다.");
            //        StartCoroutine(GetComponent<EJDoorOpen>().OpenDoor(highlight.gameObject));
            //    }
            //}

            ////Ipad
            //if (highlight.gameObject.CompareTag("IpadHomeBtn"))
            //{
            //    GetComponent<EJPropIpad>().turnONOFFScreen();
            //}



            #endregion
        }
        else
        {
            if (highlight != null)
            {
                var ol = highlight.gameObject.GetComponent<Outline>();
                ol.enabled = false;
                highlight = null;
            }
        }


    }
}
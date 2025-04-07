using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EJToDoManager : MonoBehaviour
{
    public GameObject todolist;
    public GameObject[] checks;

    public static EJToDoManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void TodolistON()
    {
        todolist.SetActive(true);         
    }

    public void ONcheckFusebox(bool a)
    {
        if (todolist.activeSelf)
        {
         checks[0].SetActive(a);     

        }
    }

    public void ONcheckDoor()
    {
        if (!checks[1].activeSelf&& todolist.activeSelf)
        {
            checks[1].SetActive(true);
        }
    }

    public void ONcheckGasValve()
    {
        if (!checks[2].activeSelf&& todolist.activeSelf)
        {
            checks[2].SetActive(true);
        }
    }

    public void ONcheckTablet()
    {
        if (!checks[3].activeSelf && todolist.activeSelf    )
        {
            checks[3].SetActive(true);
        }
    }
}

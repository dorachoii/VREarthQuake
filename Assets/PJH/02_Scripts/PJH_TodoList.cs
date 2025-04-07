using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PJH_TodoList : MonoBehaviour
{
    public GameObject TodoList;
    //public GameObject ScretOn;
    public GameObject DoorOn;
    public GameObject DoorOff;
    public GameObject GasOn;
    public GameObject GasOff;
    public GameObject WaterOn;
    public GameObject WaterOff;
    public GameObject ElectOn;
    public GameObject ElectOff;
    public GameObject ExitOn;
    public GameObject ExitOff;


    // Start is called before the first frame update
    void Start()
    {
        //GasVelve(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TodoListOn(bool a)
    {
        if (a == true)
        {
            TodoList.SetActive(true);
        }

    }
    public void Question(bool a)
    {
        // ??? 리셋되면 고치는 코드
    }
    public void DoorOpen(bool a)
    {
        if (a == true)
        {
            DoorOn.SetActive(false);
            DoorOff.SetActive(true);
        }
    }
    public void GasVelve(bool a)
    {
        if (a == true)
        {
            GasOn.SetActive(false);
            GasOff.SetActive(true);
        }
    }
    public void WaterPump(bool a)
    {       
        if (a == true)
        {
            WaterOn.SetActive(false);
            WaterOff.SetActive(true);
        }
    }
    public void FuseBox(bool a)
    {
        if (a == true)
        {
            ElectOn.SetActive(false);
            ElectOff.SetActive(true);
        }
    }

    public void Exit(bool a)
    {
        if (a == true)
        {
            ExitOn.SetActive(false);
            ExitOff.SetActive(true);
        }
    }
}

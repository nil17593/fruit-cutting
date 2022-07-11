using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateController : MonoBehaviour
{
    //public Transform plateSelected;
    public Transform slicer;
    public bool canSlice;
    public static PlateController instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray))
        //    {
        //        transform.DOLocalMove(plateSelected.position, 0.5f);
        //    }
        //MoveFruitsToTheSlicer();
    }

    public void SelectPlate()
    {
        Debug.Log("fgchgv"); 
    }

    public void MoveFruitsToTheSlicer()
    {
        Debug.Log(transform.childCount);
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).CompareTag("Fruit"))
                {
                    Debug.Log(i);
                    transform.GetChild(i).gameObject.transform.DOMove(slicer.position, 2f);                   
                }
            }
        }
    }
}

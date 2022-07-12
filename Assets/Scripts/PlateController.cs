using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateController : MonoBehaviour
{
    //public Transform plateSelected;
    public Transform slicer;
    public static bool canSlice;
    public bool thisPlate;
    public int i = 4;
    private GameObject slicingFruit;
    public static PlateController instance;

    private void Awake()
    {
        i = 3;
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
        if (GameManager.Instance.fruitsToCut.Count >= 4)
        {
            return;
        }
        else
        {
            if (transform.childCount != 0 && thisPlate)
            {

                //if (transform.GetChild(i).CompareTag("Fruit"))
                //{
                Vector3 pos = new Vector3(Random.Range(slicer.GetComponent<Collider>().bounds.min.x, slicer.GetComponent<Collider>().bounds.max.x), slicer.transform.position.y,
                Random.Range(slicer.GetComponent<Collider>().bounds.min.z, slicer.GetComponent<Collider>().bounds.max.z));
                slicingFruit = transform.GetChild(i).gameObject;

                slicingFruit.transform.DOMove(pos, 1f);
                GameManager.Instance.fruitsToCut.Add(slicingFruit.gameObject);
                slicingFruit.transform.parent = slicer;
                //slicingFruit.AddComponent(typeof(FruitSlicer));
                //slicingFruit.GetComponent<FruitSlicer>().SectionCount = 10;
                //transform.GetChild(i).
                i -= 1;
            }
        }
        //else
        //{
        //    return;
        //}
    }
}

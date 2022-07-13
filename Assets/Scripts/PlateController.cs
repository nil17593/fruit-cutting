using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateController : MonoBehaviour
{
    public Transform slicer;
    public bool canDragFruits = true;
    public static bool canSlice;
    public bool thisPlate;
    public int i;// = 4;
    private GameObject slicingFruit;
    public static PlateController instance;

    private void Awake()
    {
        canDragFruits = true;
        i = transform.childCount - 1;
        instance = this;
    }

    public void MoveFruitsToTheSlicer()
    {
        if (GameManager.Instance.fruitsToCut.Count >= 4)
        {

            return;
        }
        else
        {
            if (transform.childCount != 0 && thisPlate && GameManager.Instance.candragFruits)
            {
                Vector3 pos = new Vector3(Random.Range(slicer.GetComponent<Collider>().bounds.min.x, slicer.GetComponent<Collider>().bounds.max.x), slicer.transform.position.y,
                Random.Range(slicer.GetComponent<Collider>().bounds.min.z, slicer.GetComponent<Collider>().bounds.max.z));
                slicingFruit = transform.GetChild(i).gameObject;

                slicingFruit.transform.DOMove(pos, 1f);
                GameManager.Instance.fruitsToCut.Add(slicingFruit.gameObject);
                slicingFruit.transform.parent = slicer;
                i -= 1;
            }
        }
    }
}

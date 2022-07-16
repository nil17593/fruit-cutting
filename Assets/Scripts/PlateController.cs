using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateController : MonoBehaviour
{
    [Tooltip("In slicer GameObject FruitsPosition")]
    public bool thisPlate;
    public int i;
    private GameObject slicingFruit;
    public static PlateController instance;

    private void Awake()
    {
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
                slicingFruit = transform.GetChild(i).gameObject;
                GameManager.Instance.PlaceFruitsOnSlicer(slicingFruit);
                GameManager.Instance.fruitsToCut.Add(slicingFruit);
                i -= 1;
            }
        }
    }
}

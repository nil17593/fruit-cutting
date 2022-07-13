using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateController : MonoBehaviour
{
    [Tooltip("In slicer GameObject FruitsPosition")]
    public Transform FruitsPosition;
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
                Vector3 pos = new Vector3(Random.Range(FruitsPosition.GetComponent<Collider>().bounds.min.x, FruitsPosition.GetComponent<Collider>().bounds.max.x), FruitsPosition.transform.position.y,
                Random.Range(FruitsPosition.GetComponent<Collider>().bounds.min.z, FruitsPosition.GetComponent<Collider>().bounds.max.z));
                slicingFruit = transform.GetChild(i).gameObject;

                slicingFruit.transform.DOMove(pos, 1f);
                GameManager.Instance.fruitsToCut.Add(slicingFruit.gameObject);
                slicingFruit.transform.parent = FruitsPosition;
                i -= 1;
            }
        }
    }
}

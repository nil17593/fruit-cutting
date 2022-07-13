using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlicer : MonoBehaviour
{
    [Header("Target Fruit")]
    [Tooltip("Drag Here Fruit which will be sliced")]
    public GameObject TargetFruit;
    [Tooltip("how many pieces to create")]
    public int SectionCount;
    [Tooltip("Drag here piece of fruit")]
    public GameObject fruitCutPiece;
    [Tooltip("In slicer GameObject FruitsPosition")]
    public Transform ParentTransform;

    public bool onlyOnce = true;

    private Vector3 pos;
    private Vector3 SizeOfOriginalCube;
    private Vector3 SectionSize;
    private Vector3 FillStartPosition;
    
    private GameObject SubCube;
    

    public static FruitSlicer instance;

    void Start()
    {
        instance = this;
        onlyOnce = true;
        if (TargetFruit == null)
            TargetFruit = gameObject;

        SizeOfOriginalCube = TargetFruit.transform.lossyScale;
        SectionSize = new Vector3(
            SizeOfOriginalCube.x / SectionCount,
            SizeOfOriginalCube.y / SectionCount,
            SizeOfOriginalCube.z / SectionCount
            );

        FillStartPosition = TargetFruit.transform.TransformPoint(new Vector3(-0.5f, 0.5f, -0.5f))
                          + TargetFruit.transform.TransformDirection(new Vector3(SectionSize.x, -SectionSize.y, SectionSize.z) / 2.0f);

    }

  

    public void CutFruits()
    {
        if (onlyOnce)
        {
            for (int i = 0; i < SectionCount; i++)
            {
                pos = new Vector3(Random.Range(TargetFruit.GetComponent<Collider>().bounds.min.x, TargetFruit.GetComponent<Collider>().bounds.max.x), TargetFruit.transform.position.y,
                    Random.Range(TargetFruit.GetComponent<Collider>().bounds.min.z, TargetFruit.GetComponent<Collider>().bounds.max.z));
                SubCube = GameObject.Instantiate(fruitCutPiece) as GameObject;
                SubCube.transform.localScale = SectionSize;
                SubCube.gameObject.AddComponent<Rigidbody>();
                SubCube.gameObject.tag = "Piece";
                GameManager.Instance.fruitPieces.Add(SubCube);
                SubCube.transform.position = pos;
                SubCube.transform.rotation = TargetFruit.transform.rotation;
                SubCube.transform.SetParent(ParentTransform);
            }
        }
        onlyOnce = false;
        //Destroy(TargetFruit);
        //TargetFruit.SetActive(false);


        //foreach (Transform piece in ParentTransform)
        //{
            
        //    piece.gameObject.tag = "Piece";
        //    GameManager.Instance.fruitPieces.Add(piece.gameObject);
        //    if (piece.GetComponent<Rigidbody>() != null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        piece.gameObject.AddComponent<Rigidbody>();
        //    }
        //    //subCuboid.gameObject.GetComponent<Rigidbody>().freezeRotation = true;// = Vector3.zero;
        //    //subCuboid.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //}
    }

    IEnumerator FrrezRotation(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        go.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }
}

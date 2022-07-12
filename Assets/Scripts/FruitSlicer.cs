using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlicer : MonoBehaviour
{
    public GameObject TargetFruit;

    public int SectionCount;
    public GameObject fruitCutPiece;
    private Vector3 pos;
    private Vector3 SizeOfOriginalCube;
    private Vector3 SectionSize;
    private Vector3 FillStartPosition;
    private Transform ParentTransform;
    private GameObject SubCube;
    public bool onlyOnce = true;

    public static FruitSlicer instance;

    void Start()
    {
        instance = this;
        onlyOnce = true;
        if (TargetFruit == null)
            TargetFruit = gameObject;

        SizeOfOriginalCube = TargetFruit.transform.lossyScale;
        SectionSize = new Vector3(
            SizeOfOriginalCube.x / SectionCount + 0.1f,
            SizeOfOriginalCube.y / SectionCount + 0.1f,
            SizeOfOriginalCube.z / SectionCount + 0.1f
            );

        FillStartPosition = TargetFruit.transform.TransformPoint(new Vector3(-0.5f, 0.5f, -0.5f))
                          + TargetFruit.transform.TransformDirection(new Vector3(SectionSize.x, -SectionSize.y, SectionSize.z) / 2.0f);

        ParentTransform = new GameObject(TargetFruit.name + "CubeParent").transform;
    }

  

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Slicer"))
    //    {
    //      
    //    }
    //}

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
                SubCube.transform.position = pos;
                SubCube.transform.rotation = TargetFruit.transform.rotation;
                SubCube.transform.SetParent(ParentTransform);
            }
        }
        onlyOnce = false;
        //Destroy(TargetFruit);
        TargetFruit.SetActive(false);


        foreach (Transform subCuboid in ParentTransform)
        {
            subCuboid.gameObject.AddComponent<Rigidbody>();
            subCuboid.gameObject.tag = "Piece";
            //subCuboid.gameObject.GetComponent<Rigidbody>().freezeRotation = true;// = Vector3.zero;
            //subCuboid.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator FrrezRotation(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        go.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }
}

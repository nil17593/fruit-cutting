using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpoonController : MonoBehaviour
{
    public GameObject collection;
    public Transform spoonPoint;
    public FruitSlicer fruitSlicer;

    private void Update()
    {
        if (fruitSlicer.onlyOnce == false)
        {
            transform.DOMove(spoonPoint.position, 1f).OnComplete(EnableAnimator);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            collision.gameObject.transform.position= new Vector3(Random.Range(collection.GetComponent<Collider>().bounds.min.x, collection.GetComponent<Collider>().bounds.max.x), collection.transform.position.y,
                Random.Range(collection.GetComponent<Collider>().bounds.min.z, collection.GetComponent<Collider>().bounds.max.z));
            //collision.gameObject.transform.SetParent(null);
            collision.gameObject.transform.rotation = Quaternion.identity;
            collision.transform.SetParent(transform.transform);
        }
    }

    void EnableAnimator()
    {
        //transform.DORotate(transform.position +new Vector3(264f, -132f, -47f), 10f);
        this.gameObject.GetComponent<Animator>().enabled = true;
    }
}

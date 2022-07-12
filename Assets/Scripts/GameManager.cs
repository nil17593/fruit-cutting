using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum GameState {IceContainerSelection, Slicing, Pour, };//34.1 for steaming
    [Header("ENUM")]
    public GameState presentGameState;


    //[Header("FILLBAR SETTINGS")]
    //public Image ProgressFillingBar;
    //public float TimeforProgress;
    [Header("Image")]
    public Image IceContainerImage;


    [Header("BOOLS")]
    public bool canProgress;
    public bool onlyOnce;
    public bool canHandleTouch;
    public bool bottleSleceted;

    //public int multiplyVal;

    [Header("GAMEOBJECTS")]
    public GameObject IceContainer;
    public GameObject Slicer;
    public GameObject [] plateControllers;
    public GameObject slicerBlade;
    public RectTransform plates;
    public GameObject BottleCap;
    public GameObject spoon;
    //public int i = 0;


    public List<GameObject> fruitsToCut = new List<GameObject>();
    public List<GameObject> fruitPieces = new List<GameObject>();
    public static GameManager Instance;


    void Start()
    {
        Instance = this;
        canHandleTouch = true;
        IceContainer.SetActive(false);
        spoon.SetActive(false);
        //Slicer.SetActive(false);
        //foreach (GameObject plate in plateControllers)
        //{
        //    plate.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (canProgress)
        {
            if (presentGameState == GameState.IceContainerSelection)
            {

            }

            if (presentGameState == GameState.Slicing)
            {

            }

            if (presentGameState == GameState.Pour)
            {

            }

            else
            {
                if (!onlyOnce)
                {
                    onlyOnce = true;
                    if (presentGameState == GameState.IceContainerSelection)
                    {
                        StartCoroutine(NextStep("Slicing"));
                    }
                    else if (presentGameState == GameState.Slicing)
                    {
                        //StartCoroutine(NextStep("Pour"));

                    }
                    //else if (presentGameState == GameState.Dipping)
                    //{
                    //    StartCoroutine(NextStep("Steaming"));

                    //}
                    //else if (presentGameState == GameState.Steaming)
                    //{
                    //    StartCoroutine(NextStep("End"));
                    //}
                    //else
                    //{

                    //}
                }
                canProgress = false;
            }
        }

        if (canHandleTouch)
        {
            if (Input.GetMouseButton(0))
            {
                canProgress = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                canProgress = false;
            }
        }

    }

    public void CutFruits()
    {
        if (fruitsToCut.Count >= 4)
        {
            slicerBlade.transform.DOLocalRotate(new Vector3(0, slicerBlade.transform.rotation.y, slicerBlade.transform.rotation.z), 1f);
            foreach (GameObject fruit in fruitsToCut)
            {
                Destroy(fruit);
            }
            fruitsToCut.Clear();        
            StartCoroutine(SliceFruitsCoroutine());
            StartCoroutine(NextStep("Pour"));
        }
    }

    public void Pouring()
    {
        if (presentGameState == GameState.Pour)
        {
            Slicer.transform.DOLocalMove(new Vector3(-6f, 2.2f, 0f), 2f).OnComplete(() =>
            {
                plates.DOAnchorPos(new Vector3(8.8f, 10.57f, -4.28f), 2f);
                IceContainer.transform.DOLocalMove(new Vector3(0f, 11.65f, -1.5f), 2f);
            });
            //spoon.SetActive(true);

            //BottleCap.transform.DOLocalRotate(new Vector3(0, 0, 90f), 1f);
            //foreach (GameObject piece in fruitPieces)
            //{
            //    Vector3 pos = new Vector3(Random.Range(spoon.GetComponent<Collider>().bounds.min.x, spoon.GetComponent<Collider>().bounds.max.x), spoon.transform.position.y,
            //        Random.Range(spoon.GetComponent<Collider>().bounds.min.z, spoon.GetComponent<Collider>().bounds.max.z));
            //    piece.transform.position = pos;
            //}
        }
            
    }

    IEnumerator SliceFruitsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        FruitSlicer.instance.CutFruits();
    }

    public void EnableIceContainer()
    {
        presentGameState = GameState.IceContainerSelection;
        IceContainer.SetActive(true);
        EnablePlates();
        //IceContainerImage.gameObject.SetActive(false);
        //StartCoroutine(NextStep("Slicing"));
    }

    void EnablePlates()
    {
        plates.DOAnchorPos(new Vector3(-1.21f, 10.57f, -4.28f),2f);
    }

    public IEnumerator NextStep(string name)
    {
        canHandleTouch = false;
        yield return new WaitForSeconds(2);
        GameStateSelection(name);
    }


    public void GameStateSelection(string step)
    {
        switch (step)
        {
            case "IceContainerSelection":
                break;
            case "Slicing":
                presentGameState = GameState.Slicing;
                onlyOnce = false;
                StartCoroutine(Enable_Touch(3));
                break;
            case "Pour":
                presentGameState = GameState.Pour;
                Pouring();
                //Camera.main.transform.DOLocalMove(new Vector3(2.12f, 15f, -2.80f), 2).        
                onlyOnce = false;
                StartCoroutine(Enable_Touch(3));
                break;
        }
    }

    

    //End State
    public void EndState()
    {
        canHandleTouch = false;
    }

    //public void Slicing()
    //{
    //    Slicer.SetActive(true);
    //    foreach (GameObject plate in plateControllers)
    //    {
    //        plate.gameObject.SetActive(true);
    //    }
    //}

    public IEnumerator Enable_Touch(float sec)
    {
        yield return new WaitForSeconds(sec);
        canHandleTouch = true;
    }
}
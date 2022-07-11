using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum GameState { None, IceContainerSelection, Slicing, Pour, };//34.1 for steaming
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

    //public int multiplyVal;

    [Header("GAMEOBJECTS")]
    public GameObject IceContainer;
    public GameObject Slicer;
    public GameObject [] plateControllers;
    public GameObject slicerBlade;
    public RectTransform plates;
    public int i = 0;


    public List<GameObject> fruitsToCut = new List<GameObject>();
    public static GameManager Instance;


    void Start()
    {
        Instance = this;
        canHandleTouch = true;
        IceContainer.SetActive(false);
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
                    //else if (presentGameState == GameState.Pour)
                    //{
                    //    StartCoroutine(NextStep("Dipping"));

                    //}
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
        if (presentGameState == GameState.Slicing)
        {
            if (fruitsToCut.Count >= 3)
            {
                slicerBlade.transform.DOLocalRotate(new Vector3(0, slicerBlade.transform.rotation.y, slicerBlade.transform.rotation.z), 1f);
            }
        }
    }

    public void EnableIceContainer()
    {
        presentGameState = GameState.IceContainerSelection;
        IceContainer.SetActive(true);
        onlyOnce = false;
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
        yield return new WaitForSeconds(1);
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
                Slicer.SetActive(true);
                plateControllers[i].gameObject.SetActive(true);
                StartCoroutine(Enable_Touch(3));
                i += 1;
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
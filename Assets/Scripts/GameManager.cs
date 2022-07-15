using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum GameState { IceContainerSelection, Slicing, Pour, };
    [Header("ENUM")]
    public GameState presentGameState;

    //[Header("FILLBAR SETTINGS")]
    //public Image ProgressFillingBar;
    //public float TimeforProgress;
    //[Header("Image")]

    [Header("BOOLS")]
    public bool canProgress;
    public bool onlyOnce;
    public bool canHandleTouch;
    public bool bottleSleceted;
    public bool canPour;
    public bool candragFruits;
    public bool selectPlates;

    [Header("GAMEOBJECTS")]
    public GameObject IceContainer;
    public GameObject Slicer;
    public GameObject[] plateControllers;
    public GameObject slicerBlade;
    public RectTransform plates;
    public GameObject BottleCap;
    public Transform fruitPiecesTransformBottle;
    public GameObject plateOfPieces;
    public Transform pieceParentTransform;
    //public int i = 0;

    [Header("LISTS for Sliced Fruits")]
    public List<GameObject> fruitsToCut = new List<GameObject>();
    public List<GameObject> fruitPieces = new List<GameObject>();
    public List<GameObject> piecesInBottle = new List<GameObject>();

    [Header("COUNTS")]
    public int count = 0;
    public int piecesCount;


    public static GameManager Instance;


    void Start()
    {
        selectPlates = false;
        candragFruits = true;
        count = 0;
        Instance = this;
        canHandleTouch = true;
        canPour = false;
        IceContainer.SetActive(false);
    }

    void Update()
    {
        if (canProgress)
        {
                
        }

        //if (canHandleTouch)
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        canProgress = true;
        //    }
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        canProgress = false;
        //    }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            if (presentGameState == GameState.Pour)
            {
                if (canPour)
                {
                    PutPiecesInBottle();
                }
            }
        }
    }

    //Swipemanager Swipe Down Event
    public void CutFruits()
    {
        candragFruits = false;
        if (fruitsToCut.Count >= 4)
        {
            count += 1;
            slicerBlade.transform.DOLocalRotate(new Vector3(0, slicerBlade.transform.rotation.y, slicerBlade.transform.rotation.z), 1f);
            foreach (GameObject fruit in fruitsToCut)
            {
                fruit.SetActive(false);
                //Destroy(fruit);
            }
            fruitsToCut.Clear();
            StartCoroutine(SliceFruitsCoroutine());
            StartCoroutine(OpenSlicer());

            if (count == plates.GetComponentsInChildren<PlateController>().Length)
            {
                StartCoroutine(NextStep("Pour"));
            }
        }
    }

    public IEnumerator OpenSlicer()
    {
        if (fruitsToCut.Count <= 0 && count != plates.GetComponentsInChildren<PlateController>().Length)
        {
            yield return new WaitForSeconds(2f);
            slicerBlade.transform.DOLocalRotate(new Vector3(27.3f, slicerBlade.transform.rotation.y, slicerBlade.transform.rotation.z), 1f).OnComplete(() =>
            {
                FruitSlicer.instance.onlyOnce = true;
                candragFruits = true;
            }
            );
        }
    }

    public void Pouring()
    {
        if (presentGameState == GameState.Pour)
        {
            Slicer.transform.DOLocalMove(new Vector3(-6f, 2.2f, 0f), 2f).OnComplete(SetupForPouring);
            BottleCap.transform.DOLocalRotate(new Vector3(0, 0, 90f), 1f);
        }
    }

    public void SetupForPouring()
    {
        foreach (GameObject piece in fruitPieces)
        {
            Vector3 pos = new Vector3(Random.Range(plateOfPieces.GetComponent<Collider>().bounds.min.x, plateOfPieces.GetComponent<Collider>().bounds.max.x),
                plateOfPieces.transform.position.y + 0.5f, Random.Range(plateOfPieces.GetComponent<Collider>().bounds.min.z, plateOfPieces.GetComponent<Collider>().bounds.max.z));
            piece.transform.position = pos;
            piece.transform.SetParent(pieceParentTransform);
            piece.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        plates.DOAnchorPos(new Vector3(8.8f, 10.57f, -4.28f), 2f);
        IceContainer.transform.DOLocalMove(new Vector3(0f, 11.65f, -1.5f), 2f);

        plateOfPieces.transform.DOLocalMove(new Vector3(0.1f, 10.47f, -3.87f), 2f).OnComplete(() =>
        {
            canPour = true;
        });
    }

    void PutPiecesInBottle()
    {
        if (fruitPieces.Count > 3)
        {
            for (piecesCount = 4; piecesCount >= 0; piecesCount--)
            {
                fruitPieces[piecesCount].gameObject.transform.position = fruitPiecesTransformBottle.transform.position;
                fruitPieces[piecesCount].transform.SetParent(IceContainer.transform);
                piecesInBottle.Add(fruitPieces[piecesCount]);
                fruitPieces.RemoveAt(piecesCount);
                UIManager.instance.UpdateCountOfPieces();
            }
            piecesCount = 4;
            return;
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
    }

    void EnablePlates()
    {
        plates.DOAnchorPos(new Vector3(-1.21f, 10.57f, -4.28f), 2f).OnComplete(() =>
        {
            selectPlates = true;
        });
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
                //onlyOnce = false;
                StartCoroutine(Enable_Touch(3));
                break;
        }
    }

    //End State
    public void EndState()
    {
        canHandleTouch = false;
    }

    public IEnumerator Enable_Touch(float sec)
    {
        yield return new WaitForSeconds(sec);
        canHandleTouch = true;
    }
}

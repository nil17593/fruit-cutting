using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum GameState { IceContainerSelection, Slicing, Pour, ShakeBottle, Freezing,};
    [Header("ENUM")]
    public GameState presentGameState;

    [Header("FILLBAR SETTINGS")]
    public Image ProgressFillingBar;
    public float Timeforfreez = 5f;
    public float multiplyVal;

    [Header("BOOLS")]
    public bool canProgress;
    public bool onlyOnce;
    public bool canHandleTouch;
    public bool bottleSleceted;
    public bool canPour;
    public bool candragFruits;
    public bool selectPlates;
    public bool canFreez;

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
    public GameObject Freez;
    public GameObject freezDrawer;
    public Transform freezTransform;
    public Transform bottleTransform;
    //public int i = 0;

    [Header("LISTS for Sliced Fruits")]
    public List<GameObject> fruitsToCut = new List<GameObject>();
    public List<GameObject> fruitPieces = new List<GameObject>();
    public List<GameObject> piecesInBottle = new List<GameObject>();
    public Transform[] FruitsPosition;

    [Header("COUNTS")]
    public int count = 0;
    public int piecesCount;
    public int fruitPos = 0;

    public static GameManager Instance;


    void Start()
    {
        IceContainer.GetComponent<BottleController>().enabled = false;
        fruitPos = 0;
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
        if (presentGameState == GameState.IceContainerSelection)
        {
            multiplyVal = 1f;
        }

        if (presentGameState == GameState.Slicing)
        {
            multiplyVal = 0.17f;
        }
        if (presentGameState == GameState.Pour)
        {
            multiplyVal = 0.04f;
        }

        if (presentGameState == GameState.ShakeBottle)
        {
            if (ProgressFillingBar.fillAmount >= 1f)
            {
                IceContainer.GetComponent<BottleController>().enabled = false;
                IceContainer.transform.rotation = Quaternion.identity;
                IceContainer.transform.DOLocalMove(bottleTransform.position, 0.5f).OnComplete(() =>
                {
                    //if(presentGameState==GameState.ShakeBottle)
                    StartCoroutine(NextStep("Freezing"));
                });

            }
        }

        if (presentGameState == GameState.Freezing)
        {
            multiplyVal = 2f;
        }

       


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

        if (Input.GetMouseButton(0))
        {
            if (presentGameState == GameState.Freezing)
            {
                if (canFreez && Timeforfreez > 0)
                {
                    Timeforfreez -= Time.deltaTime;
                    ProgressFillingBar.fillAmount += 0.4f / multiplyVal * Time.deltaTime;
                }
                //if (Timeforfreez <= 0)
                //{
                //    StartCoroutine(NextStep("Final"));
                //}
               
            }
        }

    }

    #region Slicing
    //Swipemanager Swipe Down Event
    public void CutFruits()
    {
        if (presentGameState == GameState.Slicing)
        {
            ProgressFillingBar.fillAmount += multiplyVal;// * Time.deltaTime;// * plates.GetComponentsInChildren<PlateController>().Length;
            if (fruitsToCut.Count >= 4)
            {
                candragFruits = false;
                count += 1;
                slicerBlade.transform.DOLocalRotate(new Vector3(0, slicerBlade.transform.rotation.y, slicerBlade.transform.rotation.z), 1f);
                foreach (GameObject fruit in fruitsToCut)
                {
                    fruit.SetActive(false);
                    //Destroy(fruit);
                }
                fruitPos = 0;
                fruitsToCut.Clear();
                StartCoroutine(SliceFruitsCoroutine());
                StartCoroutine(OpenSlicer());

                if (count == plates.GetComponentsInChildren<PlateController>().Length)
                {
                    StartCoroutine(NextStep("Pour"));
                }
            }
        }
    }

    public void PlaceFruitsOnSlicer(GameObject go)
    {
        Transform pos = FruitsPosition[fruitPos].transform;
        go.transform.DOMove(pos.position, 1f);
        go.transform.parent = FruitsPosition[fruitPos];
        fruitPos += 1;
    }

    IEnumerator SliceFruitsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        FruitSlicer.instance.CutFruits();
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


    #endregion


    #region Pouring
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
        IceContainer.transform.DOLocalMove(bottleTransform.position, 2f);

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
            ProgressFillingBar.fillAmount += multiplyVal;//* Time.deltaTime;
            piecesCount = 4;
            return;
        }
        if (fruitPieces.Count <= 0)
        {
            presentGameState = GameState.ShakeBottle;
            BottleCap.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f);
            plateOfPieces.transform.DOLocalMoveX(-6f, 1f);
            IceContainer.GetComponent<BottleController>().enabled = true;
            StartCoroutine(NextStep("ShakeBottle"));
        }
    }
    #endregion

    #region IceContainerSelecetion
    public void EnableIceContainer()
    {
        presentGameState = GameState.IceContainerSelection;
        ProgressFillingBar.fillAmount += multiplyVal;// * Time.deltaTime;/// multiplyVal;// *Time.deltaTime;
        IceContainer.SetActive(true);
        EnablePlates();
        StartCoroutine(NextStep("Slicing"));
    }

    void EnablePlates()
    {
        plates.DOAnchorPos(new Vector3(-1.21f, 10.57f, -4.28f), 2f).OnComplete(() =>
        {
            selectPlates = true;
        });
    }
    #endregion

    void PutBottleInFreez()
    {
        Freez.transform.DOLocalMove(new Vector3(-1.29f, 10.89f, -2.21f), 0.5f).OnComplete(() =>
        {
            //IceContainer.transform.DOBlendableLocalMoveBy(Freez.transform.position, 2f);
            freezDrawer.transform.DOLocalMove(new Vector3(0f, -0.08f, -0.873f), 0.5f).OnComplete(() =>
            {
                IceContainer.transform.DOJump
                    (
                        endValue: freezTransform.position,
                        jumpPower: 1,
                        numJumps: 1,
                        duration: 3f).SetEase(Ease.InOutSine).OnComplete(() =>
                        {
                            IceContainer.transform.parent = freezTransform.transform;
                            IceContainer.transform.DOLocalRotate(new Vector3(90f, 180f, 0f), 1f);

                            freezDrawer.transform.DOLocalMove(new Vector3(0f, -0.08f, 0.024f), 1f).OnComplete(() =>
                            {
                                canFreez = true;
                            });
                        });
            });
        });
    }

    void ShakeBottle()
    {
        Debug.Log(presentGameState);
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
                ProgressFillingBar.fillAmount = 0.0f;
                presentGameState = GameState.Slicing;
                break;
            case "Pour":
                ProgressFillingBar.fillAmount = 0.0f;
                presentGameState = GameState.Pour;
                Pouring();
                break;
            case "ShakeBottle":
                presentGameState = GameState.ShakeBottle;
                ProgressFillingBar.fillAmount = 0.0f;
                ShakeBottle();
                break;
            case "Freezing":
                ProgressFillingBar.fillAmount = 0.0f;
                presentGameState = GameState.Freezing;
                PutBottleInFreez();
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

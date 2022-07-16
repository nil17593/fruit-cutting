using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public RectTransform ScrollPanel;
    [Tooltip("Text Under the PLATEOFPIECES Gameobject")]
    public TextMeshProUGUI pieceCountText;
    public RectTransform tapPanel;

    [Header("Progress work UI")]
    public Image iceContainerSelection;
    public Image slicing;
    public Image pouring;
    public Image freezing;

    public static UIManager instance;

    private void Start()
    {
        instance = this;
        ScrollPanel.DOAnchorPos( new Vector2(0f,300f), 1f);
       
    }

    private void Update()
    {
        if (GameManager.Instance.presentGameState == GameManager.GameState.IceContainerSelection)
        {
            DoScaleImage(iceContainerSelection);
        }
        if (GameManager.Instance.presentGameState == GameManager.GameState.Slicing)
        {
            DoScaleImage(slicing);
            DoScaleDownImage(iceContainerSelection);
        }
        if (GameManager.Instance.presentGameState == GameManager.GameState.Pour)
        {
            DoScaleImage(pouring);
            DoScaleDownImage(slicing);
        }
        if (GameManager.Instance.presentGameState == GameManager.GameState.Freezing)
        {
            DoScaleImage(freezing);
            DoScaleDownImage(pouring);
        }
    }

    public void TweeningPanels()
    {
        ScrollPanel.DOAnchorPos(new Vector2(0f, -300f), 1f);
    }

    //called from GameManager and FruitSlicer
    public void UpdateCountOfPieces()
    {
        pieceCountText.text = GameManager.Instance.fruitPieces.Count.ToString();
    }

    public void DoScaleImage(Image image)
    {
        image.rectTransform.DOScale(1.5f , 1f);
    }
    public void DoScaleDownImage(Image image)
    {
        image.fillAmount += 100f * Time.deltaTime;
        image.rectTransform.DOScale(1f, 1f);
    }

    
}

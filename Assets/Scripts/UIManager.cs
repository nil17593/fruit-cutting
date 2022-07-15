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

    public static UIManager instance;

    private void Start()
    {
        instance = this;
        ScrollPanel.DOAnchorPos( new Vector2(0f,300f), 1f);
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
}

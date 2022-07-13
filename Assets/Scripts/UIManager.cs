using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform ScrollPanel;

    private void Start()
    {
        ScrollPanel.DOAnchorPos( new Vector2(0f,300f), 1f);
    }

    public void TweeningPanels()
    {
        ScrollPanel.DOAnchorPos(new Vector2(0f, -300f), 1f);
    }
}

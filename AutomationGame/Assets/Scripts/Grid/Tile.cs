using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, altColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;


    public Vector2 pos;

    private void Start()
    {
    }
    public void Init(bool altColorTile)
    {
        _renderer.color = altColorTile ? altColor : baseColor;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        clickedHighlight.SetActive(false);

    }

    private void OnMouseUpAsButton()
    {
        clickedHighlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        clickedHighlight.SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

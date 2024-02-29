using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    [SerializeField] private Color baseColor, altColor,occupiedColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;


    public Vector2 pos;
    public Transform worldPos;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOccupied)
        {
            _renderer.color = occupiedColor;
        }
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
        worldPos = transform;

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,Forest, OreDeposit
}

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    [SerializeField] private Color baseColor, altColor,occupiedColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] public GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;

    public Vector2Int pos;
    public Transform worldPos;
    public GridObject gridObject;

    public TileType tileType = TileType.None;

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

    public void SetActive(bool active) // unlocking land
    {
        gameObject.SetActive(active);
    }
}

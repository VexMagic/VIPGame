using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,Forest, OreDeposit
}

public class Tile : MonoBehaviour
{
    public string id;
    public bool isOccupied;

    [SerializeField] private Color baseColor, altColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] public GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;

    public Vector2Int pos;
    public Transform worldPos;
    public GridObject gridObject;

    public TileType tileType = TileType.None;

    public List<GameObject> objectsOnTile;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        objectsOnTile = new List<GameObject>();
        id = "ID:" + pos.x + "" + pos.y;
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
        Debug.Log(worldPos);

        BuildingDisplay.Instance.CloseDisplay();
        TownhallDisplay.Instance.CloseDisplay();
    }

    public void SetActive(bool active) // unlocking land
    {
        gameObject.SetActive(active);
    }


    /*private void OnTriggerEnter2D(Collision2D collision)
    {
      
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource"))
        {
            objectsOnTile.Add(collision.gameObject);
            Debug.Log("added from tile");
        }

        if (collision.CompareTag("Townhall"))
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource"))
        {
            objectsOnTile.Remove(collision.gameObject);
            Debug.Log("removed from tile");
        }
    }

}

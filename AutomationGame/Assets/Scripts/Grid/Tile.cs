using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None, Forest, OreDeposit, Dungeon, RiftCore
}

public class Tile : MonoBehaviour
{
    public string id;
    public bool isOccupied;

    [SerializeField] private Color baseColor, altColor;
    [SerializeField] public SpriteRenderer tileBase;
    [SerializeField] public GameObject resourceTile;

    [SerializeField] public GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;

    [SerializeField] private Sprite[] grassArrayLight;
    [SerializeField] private Sprite[] grassArrayDark;

    [SerializeField] private SpriteRenderer[] subTiles;
    [SerializeField] private GameObject subTilesParent;


    [SerializeField] private Sprite grassLight;
    [SerializeField] private Sprite grassDark;


    public Vector2Int pos;
    public Transform worldPos;
    public GridObject gridObject;

    public TileType tileType = TileType.None;

    public List<GameObject> objectsOnTile;

    public bool isResourceTile;


    int randomDarkGrass;
    int randomLightGrass;

    private void Start()
    {
        objectsOnTile = new List<GameObject>();
        id = "ID:" + pos.x + "" + pos.y;
    }

    public void Init(bool altColorTile)
    {
        //_renderer.color = altColorTile ? altColor : baseColor;

        if (isResourceTile)
        {
            tileBase.sprite = altColorTile ? grassLight : grassDark;
            subTilesParent.SetActive(false);        
            if(tileType == TileType.RiftCore)
            {
                Animator anim = resourceTile.AddComponent(typeof(Animator)) as Animator;
                anim.runtimeAnimatorController = resourceTile.GetComponent<ControllerHolder>().controller;
            }
           return;
        }

        for (int i = 0; i < subTiles.Length; i++) //lag?
        {
            if (!altColorTile)
            {
                randomDarkGrass = Random.Range(0, grassArrayDark.Length);
            }
            else
                randomLightGrass = Random.Range(0, grassArrayLight.Length);

            subTiles[i].sprite = altColorTile ? grassArrayLight[randomLightGrass] : grassArrayDark[randomDarkGrass];
        }

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
        highlight.SetActive(true);

    }

    private void OnMouseDown()
    {
        clickedHighlight.SetActive(true);
        highlight.SetActive(false);
        worldPos = transform;
        Debug.Log(worldPos);

        CloseAllDisplays();
    }

    private void CloseAllDisplays()
    {
        BuildingDisplay.Instance.CloseDisplay();
        TownhallDisplay.Instance.CloseDisplay();
        DungeonDisplay.Instance.CloseDisplay();
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

        if (collision.CompareTag("Townhall") || collision.CompareTag("Dungeon"))
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

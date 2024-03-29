using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public TextMeshProUGUI goldDisplay;

    private GridObject buildingToPlace;
    [SerializeField] GridManager grid;

    [SerializeField] CustomCursor customCursor;
    [SerializeField] GameObject outPut;

    [SerializeField] GameObject building2Unlock;


    Tile tile;

    public LayerMask layerMask;

    private void Start()
    {
        building2Unlock.SetActive(false);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = gold.ToString();

        if(Townhall.Instance.level > 2) //prototype
        {
            building2Unlock.SetActive(true);
        }



        BuildCheck();
        DestroyBuilding();
    }

    private void BuildCheck()
    {

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null && !customCursor.destroyMode)
        {
            Debug.Log("building");
            #region get Tile

            tile = null;
            RaycastHit2D hit = Physics2D.Raycast(customCursor.mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Tile>() != null)
                {
                    Vector2Int tilePos = hit.collider.gameObject.GetComponent<Tile>().pos;
                    tile = grid.GetTileAtPos(tilePos);
                }
            }

            #endregion

            #region Build on tile
            if (tile == null)
                return;
            if (!tile.isOccupied && gold >= buildingToPlace.cost)
            {
                if (!(buildingToPlace is BuildingObject))
                {
                    Build(tile);
                }
                else if ((buildingToPlace as BuildingObject).buildingType.tileType == tile.tileType)
                {
                    Build(tile);
                }

                //switch (tile.tileType)
                //{
                //    case TileType.None:
                //        Build("Path", tile);
                //        break;
                //    case TileType.Forest:
                //        Build("Blacksmith", tile);//placeholder

                //        break;
                //    case TileType.OreDeposit:
                //        Build("Mine", tile);
                //        break;
                //    default:
                //        break;
                //}
            }

            #endregion
        }
        else if (Input.GetMouseButtonDown(0) && buildingToPlace == null)
            Debug.Log("building null");

        if (Input.GetMouseButtonDown(1)) //cancel 
        {
            buildingToPlace = null;
            customCursor.gameObject.SetActive(false);
            customCursor.destroyMode = false;
            Cursor.visible = true;
        }
    }
    public void Build(Tile tile)
    {
        //if (buildingToPlace.name == buildingName)
        {
            GridObject buildingObject = Instantiate(buildingToPlace, tile.transform.position, Quaternion.identity);
            gold -= buildingToPlace.cost;         
            tile.isOccupied = true;
            tile.gridObject = buildingObject;
            tile.resourceTile.SetActive(false);
            buildingObject.pos = tile.pos;
            buildingObject.output = customCursor.direction;
            if (!(buildingToPlace is Path))
            {
                buildingToPlace = null;
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }

        }
    }


    public void GetBuilding(GridObject building)
    {
        customCursor.gameObject.SetActive(true);
        outPut.SetActive(true);
        customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        customCursor.GetComponent<SpriteRenderer>().color = building.GetComponent<SpriteRenderer>().color;
        customCursor.destroyMode = false;
        Cursor.visible = false;
        buildingToPlace = building;
    }

    public void CreateBuilding(Building building)
    {
        customCursor.gameObject.SetActive(true);
        outPut.SetActive(true);
        if (building.buildingSprite != null)
            customCursor.GetComponent<SpriteRenderer>().sprite = building.buildingSprite;
        
        customCursor.GetComponent<SpriteRenderer>().color = Color.white;
        customCursor.destroyMode = false;
        Cursor.visible = false;
        BuildingObject BuildingObjectToPlace = new BuildingObject();
        BuildingObjectToPlace.buildingType = building;
        BuildingObjectToPlace.cost = building.cost;
        buildingToPlace = BuildingObjectToPlace;
        if (BuildingObjectToPlace == null)
            Debug.Log("uh oh");
    }

    public void GetHammer(Image hammer)
    {
        customCursor.gameObject.SetActive(true);
        outPut.SetActive(false);

        customCursor.GetComponent<SpriteRenderer>().sprite = hammer.sprite;
        customCursor.GetComponent<SpriteRenderer>().color = hammer.color;
        customCursor.destroyMode = true;
        Cursor.visible = false;
        buildingToPlace = null;


    }

    public void DestroyBuilding()
    {

        if (Input.GetMouseButtonDown(0) && customCursor.destroyMode) //cancel (building destroy prototype, it is working, but should not be on right click)
        {
            #region get Tile

            tile = null;
            RaycastHit2D[] hit = Physics2D.RaycastAll(customCursor.mousePos, Vector2.zero, Mathf.Infinity, layerMask);

            //for (int i = 0; i < hit.Length; i++) //for debug
            //{
            //    Debug.Log("count:" + hit[i].collider.gameObject.name);

            //}

            if (hit.Length < 2)
                return;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject.GetComponent<Tile>() != null) //uses a loop instead of specific location to ensure that it will always find the correct game object
                {
                    Vector2Int tilePos = hit[i].collider.gameObject.GetComponent<Tile>().pos;
                    tile = grid.GetTileAtPos(tilePos);
                    break;
                }
            }


            if (tile.isOccupied)
            {
                /* for (int i = tile.objectsOnTile.Count -1; i >= 0; i--) //destroy resource if path is also destroyed, idky i tried this approach when the one in resourceOb already worked
                 {
                     Destroy(tile.objectsOnTile[i]);

                 }*/
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.gameObject.GetComponent<Tile>() == null)
                    {
                        Destroy(hit[i].collider.gameObject);
                        break;
                    }
                }
            }

            tile.isOccupied = false;
            tile.gridObject = null;
            if (tile.isResourceTile)
                tile.resourceTile.SetActive(true);

            #endregion


        }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldDisplay;

    private GridObject buildingToPlace;
    [SerializeField] GridManager grid;

    [SerializeField] CustomCursor customCursor;
    [SerializeField] GameObject outPut;

    Tile tile;
    int test = 1 << 6;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = "Gold: " + gold.ToString();

        BuildCheck();
        DestroyBuilding();
    }

    private void BuildCheck()
    {

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {

            #region get Tile

            tile = null;
            RaycastHit2D hit = Physics2D.Raycast(customCursor.mousePos, Vector2.zero,3f,test);
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
                switch (tile.tileType)
                {
                    case TileType.None:
                        Build("Path", tile);

                        break;
                    case TileType.Forest:
                        Build("Blacksmith", tile);//placeholder

                        break;
                    case TileType.OreDeposit:
                        Build("Mine", tile);
                        break;
                    default:
                        break;
                }
            }

            #endregion
        }

        if (Input.GetMouseButtonDown(1)) //cancel 
        {
            buildingToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
        }
    }
    public void Build(string buildingName, Tile tile)
    {
        if (buildingToPlace.name == buildingName)
        {
            GridObject buildingObject = Instantiate(buildingToPlace, tile.transform.position, Quaternion.identity);
            gold -= buildingToPlace.cost;
            //buildingToPlace = null;
            tile.isOccupied = true;
            tile.gridObject = buildingObject;
            buildingObject.pos = tile.pos;
            buildingObject.output = customCursor.direction;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
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

            RaycastHit2D[] hit = Physics2D.RaycastAll(customCursor.mousePos, Vector2.zero);

            /*for (int i = 0; i < hit.Length; i++) //for debug
            {
                Debug.Log(hit[i].collider.gameObject);

            }*/

            if (hit.Length < 2)
                return;
            if (hit[1].collider != null)
            {
                if (hit[1].collider.gameObject.GetComponent<Tile>() != null)
                {
                    Vector2Int tilePos = hit[1].collider.gameObject.GetComponent<Tile>().pos;
                    tile = grid.GetTileAtPos(tilePos);
                }
            }

            if (tile.isOccupied)
            {
               Destroy(hit[0].collider.gameObject);
            }
            tile.isOccupied = false;
            tile.gridObject = null;

            #endregion


        }
    }

}

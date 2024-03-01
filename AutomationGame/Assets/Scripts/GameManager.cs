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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = "Gold: "+gold.ToString();

        Build();
    }

    private void Build()
    {
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            #region get Tile

            Tile tile = null;

            RaycastHit2D hit = Physics2D.Raycast(customCursor.mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Vector2Int tilePos = hit.collider.gameObject.GetComponent<Tile>().pos;
                tile = grid.GetTileAtPos(tilePos);
            }

            #endregion

            #region Build on tile
            if (tile == null)
                return;
            if (!tile.isOccupied && gold >= buildingToPlace.cost)
            {
                GridObject buildingObject = Instantiate(buildingToPlace, tile.transform.position, Quaternion.identity);
                gold -= buildingToPlace.cost;
                buildingToPlace = null;
                tile.isOccupied = true;
                tile.gridObject = buildingObject;
                buildingObject.pos = tile.pos;
                buildingObject.output = customCursor.direction;
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
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

    public void GetBuilding(GridObject building)
    {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            buildingToPlace = building;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width, height;

    [SerializeField] private Vector2 offset;

    [SerializeField] private Tile tile;

    [SerializeField] private Transform cam;

    [SerializeField] private GameObject gridList;

    public Dictionary<Vector2, Tile> tiles;

    public GameObject resourceObject;

    Tile spawnedTile;

    public GameObject townhall;
    public GameObject dungeon;

    [Header("Resource Tiles")]
    #region resource tiles

    public Sprite oreVein;
    public Sprite forest;
    public Sprite riftCore;



    #endregion

    Vector3 offset3 = new Vector3(0.5f, 0.5f, 0);
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateGrid();

    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump")) //simulate land unlock
        {
            foreach (Tile _tile in tiles.Values)
            {
                _tile.SetActive(true); 
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Vector2Int minRange = new Vector2Int(8, 11);
            Vector2Int maxRange = new Vector2Int(0, 9);

            Tile tileDung = GetTileAtPos(new Vector2Int(Random.Range(minRange.x, minRange.y), Random.Range(maxRange.x, maxRange.y)));
            while (tileDung.tileType == TileType.Dungeon)
            {
                tileDung = GetTileAtPos(new Vector2Int(Random.Range(minRange.x, minRange.y), Random.Range(maxRange.x, maxRange.y)));
            }
            if (tileDung.tileType == TileType.None)
            {
                GameObject dungeonObject = Instantiate(dungeon, tileDung.transform.position, Quaternion.identity);
                dungeonObject.name = "Dungeon";
                tileDung.tileType = TileType.Dungeon;

            }

            //spawnedTile._renderer.color = Color.red;

        }

    }
    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                spawnedTile = Instantiate(tile, gridList.transform);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.pos = new Vector2Int(x, y);
             
                spawnedTile.transform.localPosition = new Vector3(x, y);
                tiles[spawnedTile.pos] = spawnedTile;

                if (spawnedTile.pos.x > 10) //locked land
                {
                    spawnedTile.SetActive(false);
                }
                if (spawnedTile.pos == new Vector2(2, 2)) //ore desposit
                {
                    /*spawnedTile.tileType = TileType.OreDeposit;
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = oreVein;
                    spawnedTile.isResourceTile = true;
                    spawnedTile.resourceTile.SetActive(true);*/
                    ChangeTilePerResource(TileType.OreDeposit, oreVein);

                }
                if (spawnedTile.pos == new Vector2(4, 5))//forest
                {
                    /*spawnedTile.tileType = TileType.Forest;
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = forest;
                    spawnedTile.isResourceTile = true;*/
                    ChangeTilePerResource(TileType.Forest, forest);

                }

                if (spawnedTile.pos == new Vector2(8, 7))//forest
                {
                    /*spawnedTile.tileType = TileType.Forest;
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = forest;
                    spawnedTile.isResourceTile = true;*/
                    ChangeTilePerResource(TileType.RiftCore, riftCore);

                }

                if (spawnedTile.pos == new Vector2(5, 2))
                {
                    GameObject townhallObject = Instantiate(townhall, spawnedTile.transform.position + offset3, Quaternion.identity);
                    townhallObject.name = "Townhall";
                }

                bool altColorTile = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0); //color seperation
                spawnedTile.Init(altColorTile);
            }
        }


        cam.transform.position = new Vector3((float)width / 2 - 0.5f + offset.x, (float)height / 2 - 0.5f + offset.y, -10); //cam center
    }

    private void ChangeTilePerResource(TileType type, Sprite typeSprite)
    {
        spawnedTile.tileType = type;
        spawnedTile.isResourceTile = true;
        spawnedTile.resourceTile.SetActive(true);
        spawnedTile.resourceTile.GetComponent<SpriteRenderer>().sprite = typeSprite;
    }

    public Tile GetTileAtPos(Vector2Int pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}

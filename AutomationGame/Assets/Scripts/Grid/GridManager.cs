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
                bool altColorTile = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0); //color seperation
                spawnedTile.Init(altColorTile);
                spawnedTile.transform.localPosition = new Vector3(x, y);
                tiles[spawnedTile.pos] = spawnedTile;

                if (spawnedTile.pos.x > 10)
                {
                    spawnedTile.SetActive(false);
                }
                if(spawnedTile.pos == new Vector2(2, 2))
                {
                    spawnedTile.tileType = TileType.OreDeposit;
                    spawnedTile._renderer.color = Color.yellow;
                }
                if (spawnedTile.pos == new Vector2(4, 5))
                {
                    spawnedTile.tileType = TileType.Forest;
                    spawnedTile._renderer.color = Color.green;
                }
            }
        }

        cam.transform.position = new Vector3((float)width/ 2 - 0.5f + offset.x, (float)height / 2 - 0.5f + offset.y, -10); //cam center
    }

    public Tile GetTileAtPos(Vector2Int pos)
    {
        if(tiles.TryGetValue(pos,out var tile))
        {
            return tile;
        }

        return null;
    }
}

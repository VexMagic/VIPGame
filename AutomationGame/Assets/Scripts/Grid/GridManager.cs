using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tile;

    [SerializeField] private Transform cam;

    [SerializeField] private GameObject gridList;

    private Dictionary<Vector2, Tile> tiles;//maybe not needed since each tile holds their own pos data

    Tile spawnedTile;

    

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
                spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.pos = new Vector2(x, y);
                bool altColorTile = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0); //color seperation
                spawnedTile.Init(altColorTile);
                spawnedTile.transform.parent = gridList.transform;
                tiles[spawnedTile.pos] = spawnedTile;
                if (spawnedTile.pos.x > 10)
                {
                    spawnedTile.SetActive(false);
                }
            }
        }

        cam.transform.position = new Vector3((float)width/ 2 - 0.5f, (float)height / 2 - 0.5f, -10); //cam center
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        if(tiles.TryGetValue(pos,out var tile))
        {
            return tile;
        }

        return null;
    }
}

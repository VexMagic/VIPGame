using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] private float speed;

    private Storage.Resource resource;

    private Vector2Int direction;
    private Tile currentTile;
    private Vector2Int startingPos;
    private bool reachedMiddle;

    public void SetValues(Vector2Int pos, Storage.Resource resource)
    {
        this.resource = resource;

        startingPos = pos;
        currentTile = GridManager.Instance.GetTileAtPos(pos);
        transform.position = new Vector3(pos.x, pos.y);
        reachedMiddle = true;
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (!currentTile.isOccupied)
        {
            direction = Vector2Int.zero;
        }
        else
        {
            transform.position = new Vector3(currentTile.pos.x, currentTile.pos.y);

            switch (currentTile.gridObject.output)
            {
                case GridObject.Direction.Up:
                    direction = Vector2Int.up;
                    break;
                case GridObject.Direction.Down:
                    direction = Vector2Int.down;
                    break;
                case GridObject.Direction.Left:
                    direction = Vector2Int.left;
                    break;
                case GridObject.Direction.Right:
                    direction = Vector2Int.right;
                    break;
            }

            if (!GridManager.Instance.GetTileAtPos(currentTile.pos + direction).isOccupied)
            {
                direction = Vector2Int.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (direction == Vector2Int.zero)
        {
            ChangeDirection();
            if (direction == Vector2Int.zero)
                return;
        }

        transform.position = transform.position + new Vector3(direction.x * speed, direction.y * speed);

        if (IsValueBetween(currentTile.pos, currentTile.pos + direction, transform.position) && reachedMiddle)
        {
            if (currentTile.gridObject is BuildingObject && startingPos != currentTile.pos)
            {
                currentTile.gridObject.InputResource(resource);
                Destroy(gameObject);
            }
            reachedMiddle = false;
            ChangeDirection();
        }

        Vector2Int tempPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        if (currentTile.pos != tempPos)
        {
            reachedMiddle = true;
            currentTile = GridManager.Instance.GetTileAtPos(tempPos);
        }
    }

    private bool IsValueBetween(Vector2 p1, Vector2 p2, Vector2 m)
    {
        return IsValueBetween(p1.x, p2.x, m.x) && IsValueBetween(p1.y, p2.y, m.y);
    }

    private bool IsValueBetween(float p1, float p2, float m)
    {
        return ((p1 <= m) && (m <= p2)) || ((p2 <= m) && (m <= p1));
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rayDistance;

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
        if (Hit())
        {
            Destroy(gameObject);
        }
    }

    private void ChangeDirection()
    {
        if (!currentTile.isOccupied) //check if there is something in the output direction
        {
            direction = Vector2Int.zero;
        }
        else
        {
            //set movement direction
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
            else if (GridManager.Instance.GetTileAtPos(currentTile.pos + direction).gridObject is BuildingObject)
            {
                bool acceptsResource = false;
                GridObject targetObject = GridManager.Instance.GetTileAtPos(currentTile.pos + direction).gridObject;
                foreach (var item in (targetObject as BuildingObject).buildingType.input)
                {
                    if (item.resource == resource)
                        acceptsResource = true;
                }
                if (!acceptsResource)
                    direction = Vector2Int.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        //if standing still, check if it can start moving
        if (direction == Vector2Int.zero)
        {
            ChangeDirection();
            if (direction == Vector2Int.zero)
                return;
        }

        //move if no resource objects are infront
        if (!Hit())
            transform.position = transform.position + new Vector3(direction.x * speed, direction.y * speed);

        //check if it is in the middle of a tile
        if (IsValueBetween(currentTile.pos, currentTile.pos + direction, transform.position) && reachedMiddle)
        {
            //check if it entered a building
            if (currentTile.gridObject is BuildingObject && startingPos != currentTile.pos)
            {
                currentTile.gridObject.InputResource(resource);
                Destroy(gameObject);
            }

            //change direction to match the paths direction
            reachedMiddle = false;
            ChangeDirection();
        }

        //get current tile
        Vector2Int tempPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        if (currentTile.pos != tempPos)
        {
            reachedMiddle = true;
            currentTile = GridManager.Instance.GetTileAtPos(tempPos);
        }
    }

    private bool Hit()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + (Vector3)((Vector2)direction * rayDistance));
        Debug.DrawLine(transform.position, transform.position + (Vector3)((Vector2)direction * rayDistance));
        foreach (var hit in hits)
        {
            //Check if the ray hits a collider
            if (hit.collider != null)
            {
                //Check if the ray hit a resource object
                if (hit.collider.tag == gameObject.tag && hit.collider.gameObject != gameObject)
                {
                    return true;
                }
            }
        }
        return false;
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

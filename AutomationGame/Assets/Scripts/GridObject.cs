using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }

    public int cost;
    public Direction output;
    public Vector2Int pos;
    [SerializeField] protected GameObject outputArrow;
    
    protected virtual void Start()
    {
        SetRotation();
        DayManager.instance.AddBuilding(this);
    }

    public void SetRotation()
    {
        outputArrow.transform.eulerAngles = new Vector3(0, 0, DirectionToFloat(output));
    }

    public virtual void UpdateObject() { }

    public virtual void InputResource(Storage.Resource resource) { }

    public virtual void EndDay() { }

    protected bool OutputResource(Storage.Resource resource)
    {
        GridObject gridObject = GetObjectInDirection(output, pos);
        if (gridObject != null)
        {
            if (gridObject is BuildingObject)
            {
                gridObject.InputResource(resource);
                return true;
            }
            else
            {
                Instantiate(GridManager.Instance.resourceObject).GetComponent<ResourceObject>().SetValues(pos, resource);
                return true;
            }
        }
        return false;
    }

    public float DirectionToFloat(Direction direction)
    {
        float value = 0f;

        switch (direction)
        {
            case Direction.Up:
                value = 180f;
                break;
            case Direction.Down:
                value = 0f;
                break;
            case Direction.Left:
                value = 270f;
                break;
            case Direction.Right:
                value = 90f;
                break;
        }

        return value;
    }

    public Vector2Int DirectioinToGrid(Direction direction)
    {
        Vector2Int offset = Vector2Int.zero;

        switch (direction)
        {
            case Direction.Up:
                offset = Vector2Int.up;
                break;
            case Direction.Down:
                offset = Vector2Int.down;
                break;
            case Direction.Left:
                offset = Vector2Int.left;
                break;
            case Direction.Right:
                offset = Vector2Int.right;
                break;
        }

        return offset;
    }

    public GridObject GetObjectInDirection(Direction direction, Vector2Int pos)
    {
        Tile tile = GridManager.Instance.GetTileAtPos(pos + DirectioinToGrid(direction));
        if (!tile.isOccupied)
            return null;

        return tile.gridObject;
    }
}

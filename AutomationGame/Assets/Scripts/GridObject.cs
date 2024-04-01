using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GridObject : MonoBehaviour
{
    public enum Direction { Up, Right, Down, Left }

    public int cost;
    public Direction output;
    public Vector2Int pos;
    [SerializeField] protected GameObject outputArrow;
    protected GameObject highlight;
    protected GameObject clickedHighlight;


    protected virtual void Start()
    {
        highlight = gameObject.transform.Find("Highlight").gameObject;
        clickedHighlight = gameObject.transform.Find("clickedHighlight").gameObject;
        SetRotation();
        DayManager.instance.AddBuilding(this);
    }

    public virtual void SetRotation()
    {
        outputArrow.transform.eulerAngles = new Vector3(0, 0, DirectionToFloat(output));

        GridObject gridObject = GetObjectInDirection(output, pos);
        if (gridObject is Path)
        {
            Direction newInput = (Direction)(((int)output + 2) % 4);
            (gridObject as Path).input = newInput;
            (gridObject as Path).inputArrow.transform.eulerAngles = new Vector3(0, 0, DirectionToFloat(newInput));
        }
    }

    public virtual void UpdateObject() { }

    public virtual void InputResource(Storage.Resource resource) { }

    public virtual void EndDay() { }

    protected bool OutputResource(Storage.Resource resource, Direction direction)
    {
        GridObject gridObject = GetObjectInDirection(direction, pos);
        if (gridObject != null)
        {
            if (gridObject is BuildingObject)
            {
                gridObject.InputResource(resource);
                return true;
            }
            else if (gridObject is Path)
            {
                if (((int)(gridObject as Path).input + 2) % 4 == (int)direction)
                {
                    ResourceObject resourceObject = Instantiate(GridManager.Instance.resourceObject).GetComponent<ResourceObject>();
                    return resourceObject.SetValues(pos, resource);
                }
            }
            else if (gridObject is Filter)
            {
                if (gridObject.output == direction)
                {
                    ResourceObject resourceObject = Instantiate(GridManager.Instance.resourceObject).GetComponent<ResourceObject>();
                    return resourceObject.SetValues(pos, resource);
                }
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

    public Vector2Int DirectionToGrid(Direction direction)
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
        Tile tile = GridManager.Instance.GetTileAtPos(pos + DirectionToGrid(direction));
        if (!tile.isOccupied)
            return null;

        return tile.gridObject;
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
        GameManager.Instance.CloseAllDisplays();
    }

}


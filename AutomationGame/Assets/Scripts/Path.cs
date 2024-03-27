using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : GridObject
{
    //[SerializeField] Storage.Resource currentResource;
    //[SerializeField] bool hasResource;
    [SerializeField] GameObject highlight;
    public Direction input;
    public GameObject inputArrow;

    public override void SetRotation()
    {
        base.SetRotation();

        for (int i = 0; i < 4; i++) 
        {
            Tile tile = GridManager.Instance.GetTileAtPos(pos + DirectionToGrid((Direction)i));
            if (tile.isOccupied)
            {
                if (tile.gridObject.pos + DirectionToGrid(tile.gridObject.output) == pos)
                {
                    input = (Direction)i;
                    break;
                }
            }

            //Debug.Log((Direction)((i + 2) % 4) + "-" + (Direction)i);
            if (i == 3)
            {
                input = (Direction)(((int)output + 2) % 4);
            }
        }

        inputArrow.transform.eulerAngles = new Vector3(0, 0, DirectionToFloat(input));
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);

    }

    //public override void InputResource(Storage.Resource resource)
    //{
    //    currentResource = resource;
    //    hasResource = true;
    //}

    //public override void EndDay()
    //{
    //    if (hasResource)
    //    {
    //        Debug.Log("output");
    //        OutputResource(currentResource);
    //        hasResource = false;
    //    }
    //}
}

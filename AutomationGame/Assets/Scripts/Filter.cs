using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Filter : GridObject
{
    public Storage.Resource filteredResource;

    public SpriteRenderer filterItemIcon;
    //public List<Storage.Resource> outputStorage = new List<Storage.Resource>();
    public bool selected;
    protected override void Start()
    {
        base.Start();
        switch (output)
        {
            case Direction.Up:

                filterItemIcon.transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Right:
                filterItemIcon.transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.Down:
                break;
            case Direction.Left:
                filterItemIcon.transform.localRotation = Quaternion.Euler(0, 0, 90);

                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (selected)
            filterItemIcon.sprite = Storage.instance.GetSprite(filteredResource);
    }

    public Direction GetOutputDirection(Storage.Resource resource)
    {
        Direction outputDrection;

        if (resource == filteredResource)
        {
            outputDrection = (Direction)(((int)output + 1) % 4);
        }
        else
        {
            outputDrection = output;
        }



        //Debug.Log(outputDrection);
        return outputDrection;
    }

    private void OnMouseDown()
    {
        clickedHighlight.SetActive(true);
        highlight.SetActive(false);
        FilterDisplay.Instance.SelectFilter(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : GridObject
{
    //[SerializeField] Storage.Resource currentResource;
    //[SerializeField] bool hasResource;
    [SerializeField] GameObject highlight;
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

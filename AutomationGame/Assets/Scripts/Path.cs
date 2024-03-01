using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : GridObject
{
    [SerializeField] Storage.Resource currentResource;
    [SerializeField] bool hasResource;

    public override void UpdateObject()
    {
        base.UpdateObject();
    }

    public override void InputResource(Storage.Resource resource)
    {
        currentResource = resource;
        hasResource = true;
    }

    public override void EndDay()
    {
        if (hasResource)
        {
            Debug.Log("output");
            OutputResource(currentResource);
            hasResource = false;
        }
    }
}

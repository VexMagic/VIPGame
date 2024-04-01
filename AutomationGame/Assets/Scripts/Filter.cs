using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Filter : GridObject
{
    public Storage.Resource filteredResource;

    //public List<Storage.Resource> outputStorage = new List<Storage.Resource>();
    


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


    //public override void EndDay()
    //{
    //    if (outputStorage.Count > 0)
    //    {
    //        if (outputStorage[0] == filteredResource)
    //        {
    //            if (OutputResource(outputStorage[0], (Direction)(((int)output + 1) % 4)))
    //                outputStorage.RemoveAt(0);
    //        }
    //        else
    //        {
    //            if (OutputResource(outputStorage[0], output))
    //                outputStorage.RemoveAt(0);
    //        }
    //    }
    //}

    //public override void InputResource(Storage.Resource resource)
    //{
    //    outputStorage.Add(resource);
    //}
}

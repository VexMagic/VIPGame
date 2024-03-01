using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
   
    [SerializeField] private List<InputOutput> Input;
    [SerializeField] private int Days;
    [SerializeField] private List<InputOutput> Output;
    public List<InputOutput> input => Input;
    public int days => Days;
    public List<InputOutput> output => Output;

    //public bool HasResources()
    //{
    //    foreach (var item in input)
    //    {
    //        if (!Storage.instance.HasResource(item.resource, item.amount))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}


}

[Serializable]
public class InputOutput
{
    [SerializeField] private int Amount;
    [SerializeField] private Storage.Resource Resource;
    public int amount => Amount;
    public Storage.Resource resource => Resource;
}

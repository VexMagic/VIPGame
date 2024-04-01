using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
    [SerializeField] private int unlockedLvl; // maybe

    [SerializeField] private Sprite BuildingSprite;
    [SerializeField] private TileType TileType;
    [SerializeField] private int Cost;
    [SerializeField] private List<InputOutput> Input;
    [SerializeField] private int Days;
    [SerializeField] private List<InputOutput> Output;

    public Sprite buildingSprite => BuildingSprite;
    public TileType tileType => TileType;
    public int cost => Cost;
    public List<InputOutput> input => Input;
    public int days => Days;
    public List<InputOutput> output => Output;
}

[Serializable]
public class InputOutput
{
    [SerializeField] private int Amount;
    [SerializeField] private Storage.Resource Resource;
    public int amount => Amount;
    public Storage.Resource resource => Resource;
}

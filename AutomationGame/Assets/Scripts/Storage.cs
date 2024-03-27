using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    public enum Resource { Food, Metal, Weapon, Mana, Wood, Leather, Book, Warrior, Mage, Cleric }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

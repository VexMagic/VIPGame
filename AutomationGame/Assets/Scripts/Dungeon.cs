using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int adv;

    [SerializeField] public GameObject highlight;

    void Start()
    {
    }



    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);

    }

    private void OnMouseDown()
    {
        DungeonDisplay.Instance.SelectDungeon();
    }
}

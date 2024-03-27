using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int adv;
    public int daysWorked;
    [SerializeField] public GameObject highlight;
    public int daysToYieldGold = 5;
    public int gold = 100;
    public int RNG;
    public int totalGold;
    void Start()
    {
    }

    private void Update()
    {
        totalGold = adv * gold;
    }


    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);

    }

    public void Explore() // prototype
    {
        if(daysWorked >= daysToYieldGold)
        {
            GameManager.Instance.gold += adv * gold;

            if (adv > 0)
            {
                RNG = Random.Range(0, 100);
                if (RNG <= 60) // 60%
                {
                    //lose nothing
                }
                else if (RNG > 60 && RNG <= 80) //20%
                {
                    adv -= 1;

                }
                else if (RNG > 80 && RNG <= 95) //15%
                {
                    adv -= 2;

                }
                else // 5%
                    adv -= 3;
            }

            if (adv < 0)
            {
                adv = 0;
            }

            daysWorked = 0;
        }
      
        DungeonDisplay.Instance.UpdateDungeonDisplay();

    }

    private void OnMouseDown()
    {
        DungeonDisplay.Instance.SelectDungeon();
    }
}

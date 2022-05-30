using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    GridBuilder gridBuilder;

    private void Start()
    {
        gridBuilder = GridBuilder.instance;
    }

 
    public void BuyTower (int buildindex)
    {
        gridBuilder.SetSelectedTower(buildindex);
    }

}

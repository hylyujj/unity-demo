using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellItem : MonoBehaviour
{
    private int row;
    private int col;
    private bool isPlace = false;
    private bool isSynthesie = false;
    private BasketItem basketItem;
    private SyhtnesisItem syhtnesisItem;

    public int Row { get; }
    public int Col { get; }
    public bool IsPlace { get; set; }
    public bool IsSynthesie { get; set; }
    
    public void InitItem(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void PlaceItem(BasketItem basketItem)
    {
        this.basketItem = basketItem;
        isPlace = true;
    }

    public void PlaceItem(SyhtnesisItem syhtnesisItem)
    {
        this.syhtnesisItem = syhtnesisItem;
        isPlace = true;
    }

    public void RemoveItem()
    {
        basketItem = null;
        syhtnesisItem = null;
        isPlace = false;
    }

    public SyhtnesisItem GetSyhtnesisItem()
    {
        return syhtnesisItem;
    }

    public BasketItem GetBasketItem()
    {
        return basketItem;
    } 
}

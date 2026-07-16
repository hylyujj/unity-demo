using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SynthesisItemData
{
    private string item_name;
    private int item_level;
    private Sprite item_icon;

    public SynthesisItemData()
    {
        
    }

    public SynthesisItemData(string item_name, int item_level, Sprite item_icon)
    {
        this.item_name = item_name;
        this.item_level = item_level;
        this.item_icon = item_icon;
    }

    public string GetItemName()
    {
        return item_name;
    }

    public int GetItemLevel()
    {
        return item_level;
    }

    public Sprite GetItemSprite()
    {
        return item_icon;
    }

    public void UpdateItemLevel(int item_level)
    {
        this.item_level = item_level;
    }

    public void UpdateItemName(string item_name)
    {
        this.item_name = item_name;
    }

    public void UpdateItemIcon(Sprite item_icon)
    {
        this.item_icon = this.item_icon;
    }
}

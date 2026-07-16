using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePanel : MonoBehaviour
{

    [SerializeField] 
    private Transform gridCellItemTransform;
    [SerializeField] 
    private Transform basketItemTransform;
    
    private int row = 7;
    private int col = 9;
    private readonly List<GridCellItem> gridCellItems = new();
    private Transform chessboard; 
    // Start is called before the first frame update
    void Start()
    {
        DrawChessboard();
        CreateBacketItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawChessboard()
    {
        chessboard = transform.Find("middle/chessboard");
        for (byte i = 0; i < row; i++)
        for (byte j = 0; j < col; j++)
        {
            Transform itemObj = Instantiate(
                gridCellItemTransform, chessboard
            );
            GridCellItem cellItem = itemObj.GetComponent<GridCellItem>();
            cellItem.InitItem(i, j);
            gridCellItems.Add(cellItem);
        }
    }

    private void CreateBacketItem()
    {
        System.Random random = new System.Random();
        int randomAt = random.Next(gridCellItems.Count);
        GridCellItem cellItem = gridCellItems[randomAt];
        Transform basketTransform = Instantiate(basketItemTransform, cellItem.transform);
        BasketItem basketItem = basketTransform.GetComponent<BasketItem>();
        basketItem.InitBasketItem(cellItem, this);
        cellItem.PlaceItem(basketItem);
    }
    
    public GridCellItem GetOverlapMaxArea(BasketItem basketItem)
    {
        float areaValue = 0f;
        RectTransform firstRect = basketItem.transform.GetComponent<RectTransform>();
        RectTransform secondRect;
        GridCellItem maxAreaCellItem = basketItem.GetParentGridCellItem();
        for (int i = 0; i < gridCellItems.Count; i++)
        {
            if (!gridCellItems[i].IsSynthesie)
            {
                secondRect = gridCellItems[i].transform.GetComponent<RectTransform>();
                var area = MathUtil.RectTranformsIntersectionProportion(firstRect, secondRect);
                if (area > areaValue)
                {
                    areaValue = area;
                    maxAreaCellItem = gridCellItems[i];
                }
            }
        }
        return maxAreaCellItem;
    }
    
    public GridCellItem GetOverlapMaxArea(SyhtnesisItem basketItem)
    {
        float areaValue = 0f;
        RectTransform firstRect = basketItem.transform.GetComponent<RectTransform>();
        RectTransform secondRect;
        GridCellItem maxAreaCellItem = basketItem.GetParentGridCellItem();
        for (int i = 0; i < gridCellItems.Count; i++)
        {
            if (!gridCellItems[i].IsSynthesie)
            {
                secondRect = gridCellItems[i].transform.GetComponent<RectTransform>();
                var area = MathUtil.RectTranformsIntersectionProportion(firstRect, secondRect);
                if (area > areaValue)
                {
                    areaValue = area;
                    maxAreaCellItem = gridCellItems[i];
                }
            }
        }
        return maxAreaCellItem;
    }

    public GridCellItem GetEmptyGridItem()
    {
        List<GridCellItem> emptyGridItems = new List<GridCellItem>();
        for (int i = 0; i < gridCellItems.Count; i++)
        {
            if (gridCellItems[i].IsPlace != true)
            {
                emptyGridItems.Add(gridCellItems[i]);
            }
        }
        System.Random random = new System.Random();
        int randomAt = random.Next(emptyGridItems.Count);
        GridCellItem cellItem = emptyGridItems[randomAt];
        return cellItem;
    }
}

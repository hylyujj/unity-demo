using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SyhtnesisItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private List<Sprite> itemIcons;
    private GamePanel gamePanel;
    private GridCellItem parentCellItem;
    private SynthesisItemData itemData;
        
    private InputActions inputActions;
    private bool isDragging = false;
    private bool isMoveing = false;
    private Vector2 lastMousePosition;
    private Vector2 localPosition;
    private Image itemIcon;
    private int maxLevel = 7;
    
    private List<string> itemNames = new List<string>()
    {
        "cherry", "strawberry", "orange", "apple", "banana", "pineapple", "watermelon", "durian"
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeleteSyhtnesisItemItem()
    {
        Destroy(gameObject);
    }

    private void FinishSyhtnesis()
    {
        int newLevel = itemData.GetItemLevel();
        newLevel += 1;
        itemData.UpdateItemLevel(newLevel);
        string itemName = itemNames[newLevel];
        itemData.UpdateItemName(itemName);
        Sprite itemIcon = itemIcons[newLevel];
        itemData.UpdateItemIcon(itemIcon);
        this.itemIcon.sprite = this.itemData.GetItemSprite();
    }
    
    public void InitSyhtnesisItem(GridCellItem cellItem, GamePanel gamePanel, SynthesisItemData itemData)
    {
        this.gamePanel = gamePanel;
        this.itemData = itemData;
        itemIcon = transform.GetComponent<Image>();
        itemIcon.sprite = this.itemData.GetItemSprite();
        SetGridCellItem(cellItem);
    }
    
    public void SetGridCellItem(GridCellItem cellItem)
    {
        if (parentCellItem != null)
        {
            parentCellItem.RemoveItem();
            parentCellItem = null;
        }
        parentCellItem = cellItem;
    }

    public GridCellItem GetParentGridCellItem()
    {
        return parentCellItem;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        RectTransform parentTransform = parentCellItem.transform.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentTransform, // 屏幕坐标系转为父物体坐标系下的点所以传父对象rt
            eventData.position, // 可以使用Input.MousePosition, 但是为了更准确使用ed的数据
            eventData
                .pressEventCamera, // 使用enterEventCamera可能会报错, 不是每次点击都有enter事件, 建议用press, 也可以直接获取UICamera传入. 如果Canvas使用Overlay直接传null
            out lastMousePosition
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform parentTransform = parentCellItem.transform.GetComponent<RectTransform>();
        Vector2 nowPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentTransform, // 屏幕坐标系转为父物体坐标系下的点所以传父对象rt
            eventData.position, // 可以使用Input.MousePosition, 但是为了更准确使用ed的数据
            eventData
                .pressEventCamera, // 使用enterEventCamera可能会报错, 不是每次点击都有enter事件, 建议用press, 也可以直接获取UICamera传入. 如果Canvas使用Overlay直接传null
            out nowPos
        );
        
        var moveX = nowPos.x - lastMousePosition.x;
        var moveY = nowPos.y - lastMousePosition.y;
        transform.localPosition = new Vector2(
            transform.localPosition.x + moveX,
            transform.localPosition.y + moveY
        );
        lastMousePosition = nowPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GridCellItem maxAreaCellItem = gamePanel.GetOverlapMaxArea(this);
        // 判断是否需要跟其他的可拖动的item交换位置
        // 如果交换的item是果篮
        BasketItem basketItem = maxAreaCellItem.GetBasketItem();
        if (basketItem)
        {
            // 说明需要交换位置
            basketItem.SetParentGridItem(parentCellItem);
            parentCellItem.RemoveItem();
            parentCellItem.PlaceItem(basketItem);
            transform.SetParent(maxAreaCellItem.transform);
            maxAreaCellItem.RemoveItem();
            parentCellItem = maxAreaCellItem;
            parentCellItem.PlaceItem(basketItem);
            transform.localPosition = new Vector2(0, 0);
        }
        else
        {
            SyhtnesisItem syhtnesisItem = maxAreaCellItem.GetSyhtnesisItem();
            // 说明碰撞的是可以合成的对象
            if (syhtnesisItem)
            {
                // 判断等级是否一致
                if (syhtnesisItem.GetSynthesisItemData().GetItemLevel() == GetSynthesisItemData().GetItemLevel())
                {
                    // 判断等级
                    // 如果达到最高等级
                    // 那么直接将拖动的元素，放置到原来的位置
                    if (syhtnesisItem.GetSynthesisItemData().GetItemLevel() == maxLevel)
                    {
                        transform.localPosition = new Vector2(0, 0);
                    }
                    else
                    {
                        // 等级相同！开始合成
                        // 这个拖动的元素直接消除！然后将棋盘上另一个元素的等级+1
                        DeleteSyhtnesisItemItem();
                        syhtnesisItem.FinishSyhtnesis();
                    }
                }
                else
                {
                    // 等级不同交换位置
                    syhtnesisItem.SetParentGridItem(parentCellItem);
                    parentCellItem.RemoveItem();
                    parentCellItem.PlaceItem(syhtnesisItem);
                    transform.SetParent(maxAreaCellItem.transform);
                    maxAreaCellItem.RemoveItem();
                    parentCellItem = maxAreaCellItem;
                    parentCellItem.PlaceItem(syhtnesisItem);
                    transform.localPosition = new Vector2(0, 0);
                }
            }
            else
            {
                // 说明碰撞的单元格，没有元素
                transform.SetParent(maxAreaCellItem.transform);
                parentCellItem.RemoveItem();
                parentCellItem = maxAreaCellItem;
                parentCellItem.PlaceItem(syhtnesisItem);
                transform.localPosition = new Vector2(0, 0);
            }
        }
        isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 鼠标弹起，则创建对象
    }

    public void SetParentGridItem(GridCellItem gridCellItem)
    {
        transform.SetParent(gridCellItem.transform);
        transform.localPosition = new Vector2(0, 0);
    }

    public SynthesisItemData GetSynthesisItemData()
    {
        return itemData;
    }
}

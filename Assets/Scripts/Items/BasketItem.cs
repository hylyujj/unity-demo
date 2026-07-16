using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasketItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private List<Sprite> itemIcons;
    [SerializeField]
    private Transform syhtnesisTransform;
    
    private SynthesisItemData itemData;
    
    private List<string> itemNames = new List<string>()
    {
        "cherry", "strawberry", "orange", "apple", "banana", "pineapple", "watermelon", "durian"
    };
    private const int maxItemCount = 8;
    private GamePanel gamePanel;
    private GridCellItem parentCellItem;
    
    private InputActions inputActions;
    private bool isDragging = false;
    private bool isMoveing = false;
    private Vector2 lastMousePosition;
    private Vector2 localPosition;
    
    
    
    // private void Awake()
    // {
    //     inputActions = new InputActions();
    // }
    //
    // private void OnEnable()
    // {
    //     inputActions.Enable();
    //     inputActions.UI.Press.started += OnPress;
    //     inputActions.BasketItem.Press.canceled += OnRelease;
    // }
    //
    // private void OnDisable()
    // {
    //     inputActions.UI.Press.started -= OnPress;
    //     inputActions.UI.Press.canceled -= OnRelease;
    //     inputActions.Disable();
    // }

    // private void OnPress(InputAction.CallbackContext context)
    // {
    //     isDragging = true;
    //     // 记录按下时的鼠标位置
    //     lastMousePosition = ScreenToParentLocal(
    //         inputActions.BasketItem.Drag.ReadValue<Vector2>(), Camera.main, parentCellItem.transform
    //     );
    // }
    //
    // private void OnRelease(InputAction.CallbackContext context)
    // {
    //     isDragging = false;
    //     if (isMoveing)
    //     {
    //         GridCellItem maxAreaCellItem = gamePanel.GetOverlapMaxArea(this);
    //         transform.SetParent(maxAreaCellItem.transform);
    //         parentCellItem.RemoveItem();
    //         parentCellItem = maxAreaCellItem;
    //         transform.localPosition = new Vector2(0, 0);
    //     }
    //     else
    //     {
    //         // 没有移动！说明是点击！那么就应该创建篮子里面，可以合成的对象
    //         SyhtnesisItem item = CreateNewSyhtnesisItem();
    //     }
    //     isMoveing = false;
    // }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (isDragging)
        // {
        //     // 获取当前鼠标位置
        //     Vector2 currentMousePosition = ScreenToParentLocal(
        //         inputActions.BasketItem.Drag.ReadValue<Vector2>(), Camera.main, parentCellItem.transform
        //     );
        //     
        //     if (currentMousePosition != lastMousePosition)
        //     {
        //         isMoveing = true;
        //         // 计算鼠标移动的差值
        //         Vector2 delta = currentMousePosition - lastMousePosition;
        //         transform.localPosition = new Vector2(
        //             transform.localPosition.x + delta.x,
        //             transform.localPosition.y + delta.y
        //         );
        //         // 更新上一次的鼠标位置
        //         lastMousePosition = currentMousePosition;
        //     }
        // }
    }
    
    private Vector2 ScreenToParentLocal(Vector2 screenPos, Camera camera, Transform parentTransform)
    {
        // 1. 屏幕坐标 -> 世界坐标
        Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
        // 2. 世界坐标 -> 父对象局部坐标
        return parentTransform.InverseTransformPoint(worldPos);
    }

    private SyhtnesisItem CreateNewSyhtnesisItem()
    {
        System.Random random = new System.Random();
        int index = random.Next(maxItemCount);
        string itemName = itemNames[index];
        Sprite itemIcon = itemIcons[index];
        SynthesisItemData newItemData = new SynthesisItemData(itemName, index, itemIcon);
        GridCellItem emptyGridItem = gamePanel.GetEmptyGridItem();
        Transform itemTransform = Instantiate(syhtnesisTransform, emptyGridItem.transform);
        SyhtnesisItem syhtnesisItem = itemTransform.GetComponent<SyhtnesisItem>();
        syhtnesisItem.InitSyhtnesisItem(emptyGridItem, gamePanel, newItemData);
        emptyGridItem.PlaceItem(syhtnesisItem);
        return syhtnesisItem;
    }
    
    // private void InitInputActions()
    // {
    //     inputActions = new InputActions();
    //     inputActions.Enable();
    //     inputActions.BasketItem.Press.started += OnPress;
    //     inputActions.BasketItem.Press.canceled += OnRelease;
    // }
    
    public void InitBasketItem(GridCellItem cellItem, GamePanel gamePanel)
    {
        // InitInputActions();
        itemData = new SynthesisItemData("basket", -1, null);
        this.gamePanel = gamePanel;
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
        SyhtnesisItem placeItem = maxAreaCellItem.GetSyhtnesisItem();
        if (placeItem != null)
        {
            // 说明需要交换位置
            placeItem.SetParentGridItem(parentCellItem);
            parentCellItem.RemoveItem();
            parentCellItem.PlaceItem(placeItem);
        }
        transform.SetParent(maxAreaCellItem.transform);
        maxAreaCellItem.RemoveItem();
        parentCellItem = maxAreaCellItem;
        parentCellItem.PlaceItem(placeItem);
        transform.localPosition = new Vector2(0, 0);
        isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 鼠标弹起，则创建对象
        if (isDragging != true)
        {
            // 说明是单击
            SyhtnesisItem item = CreateNewSyhtnesisItem();
        }
    }
    
    public void SetParentGridItem(GridCellItem gridCellItem)
    {
        transform.SetParent(gridCellItem.transform);
        transform.localPosition = new Vector2(0, 0);
    }
}

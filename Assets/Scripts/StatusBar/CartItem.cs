using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CartItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Transform dialogPanelTransform;
    [SerializeField] 
    private Transform shopBoxTransform;
    [SerializeField] 
    private Transform dialogBoxTransform;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 弹起，才显示对话框
        dialogPanelTransform.gameObject.SetActive(true);
        ShopDialog shopDialog = shopBoxTransform.GetComponent<ShopDialog>();
        shopDialog.InitDialogBox();
        DiamondDialog diamonddialog = dialogBoxTransform.GetComponent<DiamondDialog>();
        diamonddialog.InitDialogBox();
        shopBoxTransform.gameObject.SetActive(true);
    }
}

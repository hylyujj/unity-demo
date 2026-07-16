using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiamondItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Transform dialogPanelTransform;
    [SerializeField] 
    private Transform dialogBoxTransform;
    [SerializeField] 
    private Transform shopBoxTransform;
    
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
        DiamondDialog diamonddialog = dialogBoxTransform.GetComponent<DiamondDialog>();
        diamonddialog.InitDialogBox();
        ShopDialog shopDialog = shopBoxTransform.GetComponent<ShopDialog>();
        shopDialog.InitDialogBox();
        dialogBoxTransform.gameObject.SetActive(true);
    }
}

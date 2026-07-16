using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VitalityItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Transform dialogPanelTransform;

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
        VitalityDialog dialog = dialogBoxTransform.GetComponent<VitalityDialog>();
        dialog.InitDialogBox();
        dialogBoxTransform.gameObject.SetActive(true);
    }
}

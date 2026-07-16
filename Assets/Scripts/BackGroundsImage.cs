using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackGroundsImage : MonoBehaviour
{
    private InputActions inputActions;
    private bool isDragging = false;
    private Vector2 lastMousePosition;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Press.started += OnPress;
        inputActions.UI.Press.canceled += OnRelease;
    }
    
    private void OnDisable()
    {
        inputActions.UI.Press.started -= OnPress;
        inputActions.UI.Press.canceled -= OnRelease;
        inputActions.Disable();
    }

    private void OnPress(InputAction.CallbackContext context)
    {
        isDragging = true;
        // 记录按下时的鼠标位置
        lastMousePosition = inputActions.UI.Drag.ReadValue<Vector2>();
    }
    
    private void OnRelease(InputAction.CallbackContext context)
    {
        isDragging = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            // 获取当前鼠标位置
            Vector2 currentMousePosition = inputActions.UI.Drag.ReadValue<Vector2>();
            
            // 计算鼠标移动的差值
            Vector2 delta = currentMousePosition - lastMousePosition;
            
            // --- 在此处理拖拽逻辑 ---
            // 示例 1：如果是UI拖拽，可以直接修改 RectTransform 的 anchoredPosition
            // 示例 2：如果是世界坐标下的 3D 物体拖拽，可使用 Camera.main.ScreenToWorldPoint 转换
            // 例如：修改物体位置
            Vector2 pos = Camera.main.ScreenToWorldPoint(delta);
            transform.Translate(new Vector3(pos.x, pos.y, 0) * Time.deltaTime);
            
            // 更新上一次的鼠标位置
            lastMousePosition = currentMousePosition;
        }
    }
}

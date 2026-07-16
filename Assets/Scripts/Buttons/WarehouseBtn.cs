using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseBtn : MonoBehaviour
{
    [SerializeField]
    private Transform dialogPanelTransform;

    [SerializeField] 
    private Transform dialogBoxTransform;

    private bool isInit = false;
    private Button btnWarehouse;
    private void Awake()
    {
        if (isInit == false)
        {
            InitBtn();
            isInit = true;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitBtn()
    {
        btnWarehouse = transform.GetComponent<Button>();
        btnWarehouse.onClick.AddListener(OnOpenWarehouseDialog);
    }

    private void OnOpenWarehouseDialog()
    {
        dialogPanelTransform.gameObject.SetActive(true);
        WarehouseDialog warehouseDialog = dialogBoxTransform.GetComponent<WarehouseDialog>();
        warehouseDialog.InitDialogBox();
        warehouseDialog.gameObject.SetActive(true);
    }
}

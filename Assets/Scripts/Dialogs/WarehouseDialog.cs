using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseDialog : MonoBehaviour
{

    [SerializeField] 
    private Transform parentPanelTransform;
    private Button btnClose;
    private Button btnOrganize;

    private bool isInit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
        parentPanelTransform.gameObject.SetActive(false);
    }
    
    public void InitDialogBox()
    {
        if (isInit == false)
        {
            btnClose = transform.Find("btn_close").GetComponent<Button>();
            btnClose.onClick.AddListener(OnClose);
            btnOrganize = transform.Find("btn_organize").GetComponent<Button>();
            btnOrganize.onClick.AddListener(OnClose);
            isInit = true;
        }
    }
}

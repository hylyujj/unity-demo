using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondDialog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private Transform parentPanelTransform;

    [SerializeField] 
    private Transform shopDialogTransform;
    
    private Button btnClose;
    private Button btnToShop;

    private bool isInit = false;
    
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

    private void OnToShop()
    {
        gameObject.SetActive(false);
        shopDialogTransform.gameObject.SetActive(true);
    }
    
    public void InitDialogBox()
    {
        if (isInit == false)
        {
            btnClose = transform.Find("btn_close").GetComponent<Button>();
            btnClose.onClick.AddListener(OnClose);
            btnToShop = transform.Find("btn_to_shop").GetComponent<Button>();
            btnToShop.onClick.AddListener(OnToShop);
            isInit = true;
        }
    }
}

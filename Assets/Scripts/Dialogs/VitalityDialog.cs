using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalityDialog : MonoBehaviour
{

    [SerializeField] 
    private Transform parentPanelTransform;
    private Button btnClose;
    private Button btnConfirm;

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
            btnConfirm = transform.Find("confirm_btn").GetComponent<Button>();
            btnConfirm.onClick.AddListener(OnClose);
            isInit = true;
        }
    }
}

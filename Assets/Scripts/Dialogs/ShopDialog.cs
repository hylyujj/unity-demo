using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private Transform parentPanelTransform;

    [SerializeField] 
    private Transform diamondDialogTransform;
    
    private Button btnClose;
    private Button btnToDiamond;
    private Transform scrollTransform;

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
        ResetScrollContentPosition();
        gameObject.SetActive(false);
        parentPanelTransform.gameObject.SetActive(false);
    }

    private void OnToDiamond()
    {
        ResetScrollContentPosition();
        gameObject.SetActive(false);
        diamondDialogTransform.gameObject.SetActive(true);
    }

    private void ResetScrollContentPosition()
    {
        scrollTransform.localPosition = new Vector2(0, 0);
    }
    
    public void InitDialogBox()
    {
        if (isInit == false)
        {
            scrollTransform = transform.Find("content/Viewport/Content");
            btnClose = transform.Find("btn_close").GetComponent<Button>();
            btnClose.onClick.AddListener(OnClose);
            btnToDiamond = transform.Find("btn_to_diamond").GetComponent<Button>();
            btnToDiamond.onClick.AddListener(OnToDiamond);
            isInit = true;
        }
    }
}

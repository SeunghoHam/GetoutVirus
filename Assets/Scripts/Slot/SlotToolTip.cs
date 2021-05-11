using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Text text_ItemName;
    [SerializeField]
    private Text text_ItemDesc;

    [SerializeField]
    private Text text_TrashName;
    [SerializeField]
    private Text text_TrashDesc;
    

    // 해보는것
    private RaycastHit hitInfo; // 충돌체 정보 저장
    private void Start()
    {
    }
    public void ShowToolTip_Item(Item _item, Vector3 _pos)
    {
        if (Inventory.inventoryActivated)
        {
            //if (hitInfo.transform.tag == "Item")
            //{
                go_Base.SetActive(true);
                _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f, -go_Base.GetComponent<RectTransform>().rect.height * 0.5f, 0f);
                go_Base.transform.position = _pos;
                text_ItemName.text = _item.itemName;
                text_ItemDesc.text = _item.itemDesc;
           //}
           
        }
        HideToolTip();
    }
   public void ShowToolTip_Trash(Trash _trash, Vector3 _pos)
    {
        if (Inventory.inventoryActivated)
        {
            if (hitInfo.transform.tag == "Trash")
            {
                go_Base.SetActive(true);
                _pos += new Vector3(+go_Base.GetComponent<RectTransform>().rect.width * 0.5f, -go_Base.GetComponent<RectTransform>().rect.height * 0.5f, 0f);
                go_Base.transform.position = _pos;
                text_TrashName.text = _trash.trashName;
                text_TrashDesc.text = _trash.trashDesc;
            }
  
        }
        HideToolTip();
    }


    public void HideToolTip()
    {
            go_Base.SetActive(false);
      
    }
}

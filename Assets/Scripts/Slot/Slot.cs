using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage; // 아이템의 이미지


    // 필요한 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private SlotToolTip theSlotToolTip;


    [SerializeField]
    GameObject MouseParticle;
    //private RaycastHit hitInfo; // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크
    private void Start()
    {
    }
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
            theSlotToolTip.ShowToolTip_Item(_item, _pos);
    }
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    // 이미지의 투명도
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
            //AddItem(_item);
            item = _item;
            itemCount = _count;
            itemImage.sprite = item.itemImage;
            if (Item.ItemType.Door != item.itemType) // Item.ItemType.Key != item.itemType ||
            {
                go_CountImage.SetActive(true);
                text_Count.text = itemCount.ToString();
             
            }
            else
            {
                text_Count.text = "0";
                go_CountImage.SetActive(false);
            }

            SetColor(1);
 
    }



    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        if (item.itemType != Item.ItemType.Door)
        {
                itemCount += _count;
                text_Count.text = itemCount.ToString();

                if (itemCount <= 0)
                    ClearSlot();
        }
    }

    // 슬롯초기화
    void ClearSlot()
    {
            item = null;
            itemCount = 0;
            itemImage.sprite = null;
            SetColor(0);

            text_Count.text = "0";
            go_CountImage.SetActive(false);


    }

    public void OnPointerClick(PointerEventData eventData) // 슬롯에서 클릭햇을때 나오는 이벤트
    {

        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            if (item!=null)
            {
                if (item.itemType == Item.ItemType.NoneSelect)  
                {
                    //StartCoroutine(theWeaponMgr.ChangeWeaponCoroutine(item.weaponType, item.itemName));
                    Debug.Log("이거 안뜰거임");
                }
                else
                {
                    Debug.Log(item.itemName + "을 사용함");
                    //SetSlotCount(-1);

                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        Debug.Log("OnEndDrag");
        ClearSlot();
        text_Count.text = "0";
        SetColor(0);
        go_CountImage.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        ClearSlot();
        text_Count.text = "0";
        SetColor(0);
        go_CountImage.SetActive(false);
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
        //if (DragSlot.instance.tag == "TrashBasket")
          //  ClearSlot();

    }
    public void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if (_tempItem != null)
        {
            if (item.itemType == _tempItem.itemType)
            {
                itemCount += _tempItemCount;
    
            }
            else
                DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);


            ClearSlot();
        }
    
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    // 마우스가 슬롯에 들어갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        ShowToolTip(item, transform.position);
        
    }


    // 슬롯에서 빠져나갈 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}

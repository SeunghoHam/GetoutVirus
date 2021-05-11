using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_Key : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage; // 아이템의 이미지



    // 필요한 컴포넌트

    private WeaponMgr theWeaponMgr;

    [SerializeField]
    private SlotToolTip theSlotToolTip;


    private void Start()
    {
        theWeaponMgr = FindObjectOfType<WeaponMgr>();
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
    public void AddItem(Item _item, int _count)
    {
        //AddItem(_item);
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if (item.itemType != Item.ItemType.NoneSelect)
        {
            if (item.itemType == Item.ItemType.Key)
            {
                Debug.Log("키 획득");

            }
        }



        SetColor(1);

    }

    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        if (item.itemType == Item.ItemType.Key)
        {
            itemCount += _count;
            if (itemCount <= 0)
            {
                CanOpenDoor();
            }

        }
    }
    void CanOpenDoor()
    { 

    }

    // 마우스가 슬롯에 들어갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            ShowToolTip(item, transform.position);
    }


    // 슬롯에서 빠져나갈 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}

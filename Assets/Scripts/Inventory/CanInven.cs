using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInven : MonoBehaviour
{
    public static bool CanInvenActivated = false;

    [SerializeField]
    private GameObject go_CanInvenBase;
    [SerializeField]
    private GameObject go_SlotParent;


    private Slot[] slots;

    private RaycastHit hitInfo; // 충돌체 정보 저장
    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();

    }

    private void Update()
    {
        if (ActionCtr.CanInvenON)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryOpenCanInven();
            }
        }
    }

    public void TryOpenCanInven()
    { 
        CanInvenActivated = !CanInvenActivated;
        if (CanInvenActivated)
        {
            OpenCanInventory();
            Debug.Log("캔 인벤토리 on");
        }
        else
        {
            CloseCanInventory();
        }

    }


    public void OpenCanInventory()
    {
        GameMgr.isOpenInventory = true;
        go_CanInvenBase.SetActive(true);
    }
    public void CloseCanInventory()
    {
        GameMgr.isOpenInventory = false;
        go_CanInvenBase.SetActive(false);
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (_item.itemType != Item.ItemType.Door)
        //if (Item.ItemType.Door != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)  // 슬롯에 빈자리가 있나 확인
            {
                if (slots[i].item != null)
                {

                    if (slots[i].item.itemName == _item.itemName) // 슬롯 안에 이미 있는 아이템이라면
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }

                }

            }

            for (int i = 0; i < slots.Length; i++) // 슬롯에 빈자리가 있나 확인
            {

                //if (slots[i].item.itemName == null) // 슬롯 안에 없는 아이템임
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
        }
    }
}

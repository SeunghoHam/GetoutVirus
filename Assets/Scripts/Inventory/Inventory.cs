using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;


    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotParent;

    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();

    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();


    }
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }
    public void OpenInventory()
    {
        GameMgr.isOpenInventory = true;
        go_InventoryBase.SetActive(true);
    }
    public void CloseInventory()
    {
        GameMgr.isOpenInventory = false;
        go_InventoryBase.SetActive(false);
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

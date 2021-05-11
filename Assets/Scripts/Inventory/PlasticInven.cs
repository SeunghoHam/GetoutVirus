using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticInven : MonoBehaviour
{

    public static bool PlasticInvenActivated = false;
    [SerializeField]
    private GameObject go_PlasticInvenBase;
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
        if (ActionCtr.PlasticInvenON)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryOpenPlasticInven();
            }
        }
    }

    public void TryOpenPlasticInven()
    {
        PlasticInvenActivated = !PlasticInvenActivated;
        if (PlasticInvenActivated)
        {
            OpenPlasticInventory();
            Debug.Log("플라스틱 인벤토리 on");
        }
        else
        {
            ClosePlasticInventory();
        }

    }


    public void OpenPlasticInventory()
    {
        GameMgr.isOpenInventory = true;
        go_PlasticInvenBase.SetActive(true);
    }
    public void ClosePlasticInventory()
    {
        GameMgr.isOpenInventory = false;
        go_PlasticInvenBase.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCtr : MonoBehaviour
{
    private static bool GetKey = false;

    public static bool CanInvenON = false;
    public static bool PlasticInvenON = false;
    [SerializeField]
    private float range; // 습득 가능한 최대 거리

    private bool pickupActivated = false; // 습득 가능할 시 true

    private RaycastHit hitInfo; // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private CanInven theCanInven;
    [SerializeField]
    private PlasticInven thePlasticInven;

    private void Start()
    {
        theCanInven = GetComponent<CanInven>();
        thePlasticInven = GetComponent<PlasticInven>();
    }
    private void Update()
    {
        if (HandCtr.isActivate)
        {
            CheckItem();
            TryAction();
            TryOpen();
            CheckDoor();
            CheckTrashBasket();
        }
    }

    void TryOpen()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 문
            CheckDoor();
            PushDoor();

            // 쓰레기통
            CheckTrashBasket();
            OpenCanBakset();
            OpenPlasticBasket();

        }
    }
    void TryAction()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            CanPickUp();
            CheckItem();

        }
    }
    void CheckDoor()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Door")
            {
                DoorItemInfoAppear();

            }
        }
        else
            InfoDisappear();
    }
    void CheckTrashBasket()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "TB_CAN" || hitInfo.transform.tag == "TB_PLASTIC")
            {

                TrashBasketInfoAppear();
                CanInvenON = true;
                PlasticInvenON = true;
            }
        }
        else
        {
            InfoDisappear();
            CanInvenON = false;
            PlasticInvenON = false;
        }
    }
    void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform.tag == "Item")
            {
                if (hitInfo.transform != null) // 버그방지
                {
                    Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다");
                    theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    //theCanInven.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    Destroy(hitInfo.transform.gameObject);
                    InfoDisappear();

                }
            }
            if (hitInfo.transform.tag == "Key")
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                //theCanInven.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                GetKey = true;
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }


    void OpenCanBakset()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform.tag == "TB_CAN")
            {
                if (CanInvenON)
                {
                    if (hitInfo.transform != null)
                    {
                        Debug.Log(hitInfo.transform.GetComponent<TrashBasket>().trash.trashName + "을 열려합니다");
                        // 인벤토리 열기
                        theCanInven.TryOpenCanInven();
                        InfoDisappear();
                    }
                }
                CanInvenON = false;
            }
        }
        actionText.gameObject.SetActive(false);
    }
    void OpenPlasticBasket()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform.tag == "TB_PLASTIC")
            {
                if (PlasticInvenON)
                {
                    if (hitInfo.transform != null)
                    {
                        Debug.Log(hitInfo.transform.GetComponent<TrashBasket>().trash.trashName + "을 열려합니다");
                        thePlasticInven.TryOpenPlasticInven();
                        InfoDisappear();
                        
                    }
                }
                PlasticInvenON = false;
            }
        }
        actionText.gameObject.SetActive(false);
    }
 
    void PushDoor()
    {
        if (pickupActivated)
        {
            if (GetKey)
            {
                if (hitInfo.transform.tag == "Door")
                {
                    if (hitInfo.transform != null) // 버그방지
                    {
                        Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "을 열려합니다");
                        Destroy(hitInfo.transform.gameObject, 0.5f);
                        InfoDisappear();
                    }
                }
            }
            else if (!GetKey)
            {
                Debug.Log("문 잠겨있다");
                // 문이 잠긴  사운드
            }

        }
        actionText.gameObject.SetActive(false);
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item" || hitInfo.transform.tag == "Key")
            {
                ItemInfoAppear();

            }
        }
        else
            InfoDisappear();
    }

    void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득" + "<color=yellow>" + "(E)" + "</color>";
    }
    void DoorItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "열기" + "<color=yellow>" + "(E)" + "</color>";
    }
    void TrashBasketInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<TrashBasket>().trash.trashName + "열기" + "<color=yellow>" + "(E)" + "</color>";
    }
    void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);

    }

}

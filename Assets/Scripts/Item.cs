using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item / item")]
public class Item : ScriptableObject // 게임 오브젝트에 넣을 필요없는 오브젝트 만들기
{
    public string itemName;         // 아이템의 이름
    [TextArea]                      // 한줄 내리기 보기 편하게
    public string itemDesc;         // 아이템의 설명
    public ItemType itemType;       // 아이템의 유형
    public Sprite itemImage;        // 아이템의 이미지
    public GameObject itemPrefab;   // 아이템의 프리팹

    public void PickUp()
    {
        //SoundMgr.instance.PlaySE();
    }



    public enum ItemType
    { 
        Can,
        Plastic,
        Normal,
        Battery,
        Door,
        Key,
        Glass,
        star,
        NoneSelect,
    }

    // [SerializeField] private string Can_PickUp_Sound;
    // [SerializeField] private string Plastic_PickUp_Sound;


}

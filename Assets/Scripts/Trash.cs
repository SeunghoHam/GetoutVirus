using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Trash", menuName = "New Trash / trash")]
public class Trash : ScriptableObject
{
    public string trashName;         // 아이템의 이름
    [TextArea]                      // 한줄 내리기 보기 편하게
    public string trashDesc;         // 쓰레기통의 설명
    public TrashType trashType;      // 쓰레기통 타입
    public Sprite trashImage;        // 쓰레기통의 이미지
    public GameObject trashobejct;   // 쓰레기통의 오브젝트
    public enum TrashType
    {
        Can,
        Glass,
        Battery,
        Paper,
        Plastic,
        Nonselect,
    
    }


}

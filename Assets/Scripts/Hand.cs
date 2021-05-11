using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string HandName; // 근접무기 이름

    // 웨폰 유형
    public bool isHand;
    public bool isSword;


    public float Range;
    public float Damage;

    public float AttackDelay; 
    public float AttackDelayA; 
    public float AttackDelayB; 

    public float PickupDelay;   // 줍기 딜레이   
    public float PickupDelayA;  // 줍기 활성화 시점
    public float PickupDelayB;  // 줍기 비활성화 시점

    //public Animator L_anim;
    //public Animator R_anim;

}

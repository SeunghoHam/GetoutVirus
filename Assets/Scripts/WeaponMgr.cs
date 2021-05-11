using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponMgr : MonoBehaviour
{
    // 무기 중복 교체 실행 방지
    public static bool isChangeWeapon = false;

    // 현재 무기와 현재 무기의 애니메이션
    public static Transform currentWeapon;
    public static Animator currentWeaponAnimator;

    // 현재 무기의 타입
    [SerializeField]
    private string currentWeaponType;


    // 무기 교체 딜레이, 무기 교체가 완전히 끝난 시점
    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;


    // 손 종류 관리
    [SerializeField]
    private Sword[] swords;
    [SerializeField]
    private Hand[] hands;


    // 관리차원에서 쉽게 손 접근
    private Dictionary<string, Sword> swordDictionary = new Dictionary<string, Sword>();
    private Dictionary<string, Hand> handDictionary = new Dictionary<string, Hand>();

    [SerializeField]
    private SwordCtr theSwordCtr;
    [SerializeField]
    private HandCtr theHandCtr;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            swordDictionary.Add(swords[i].SwordName, swords[i]);
        }
        for (int i = 0; i < hands.Length; i++)  
        {
            handDictionary.Add(hands[i].HandName, hands[i]);
        }
      
    }

    private void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutine("HAND", "손"));

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //StopCoroutine(ChangeWeaponCoroutine("HAND", "손"));
                StartCoroutine(ChangeWeaponCoroutine("SWORD", "칼"));
            }
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        
        isChangeWeapon = true;
        currentWeaponAnimator.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;

      
    }

    void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "HAND":
                HandCtr.isActivate = false;
                break;
            case "SWORD":
                SwordCtr.isActivate = false;
                break;
            default:
                break;
        }
    }

    void WeaponChange(string _type, string _name)
    {
        if (_type == "SWORD")
            theSwordCtr.SwordChange(swordDictionary[_name]);
        else if (_type == "HAND")
            theHandCtr.HandChange(handDictionary[_name]);
    }
}


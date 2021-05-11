using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtr : MonoBehaviour
{  
    // 활성화 여부
    public static bool isActivate = true;
    //public static bool isActivate = false;
    [SerializeField]
    private Hand currentHand;
    [SerializeField]
    private Crosshair crosshair;

    //공격중?
    private bool isAttack = false;
    private bool isSwing = false;


    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    //줍는중?
    //private bool isPickUp = false;


    [SerializeField]
    private Animator L_anim;
    [SerializeField]
    private Animator R_anim;


    public void WalkingAnimation(bool _flag)
    {
        L_anim.SetBool("Walking", _flag);
        R_anim.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        L_anim.SetBool("Running", _flag);
        R_anim.SetBool("Running", _flag);
    }

    private void Update()
    {
        if(isActivate)
            TryAttack();
    }

    void TryAttack()
    {
        if(!Inventory.inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
   
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true; // 중복실행 방지

        R_anim.SetTrigger("Interaction"); // 애니메이션 실행
        //currentHand.Crosshair_anim.SetTrigger("Interaction"); // 애니메이션 실행 


        yield return new WaitForSeconds(currentHand.AttackDelayA); // 약간의 딜레이
        isSwing = true;
        // 공격 활성화 시점

        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(currentHand.AttackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.AttackDelay- currentHand.AttackDelayA - currentHand.AttackDelayB);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing) 
        {
            if (CheckObject())
            {
                // 충돌 했음
                isSwing = false; // 적중 1번만
                //Debug.Log(hitInfo.transform.name);
            }
            else
            {
                // 충돌 안함
            }

            yield return null;
        }
    }

    bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.Range, layerMask)); // out hitinfo = 충돌체가 있다면 충돌체의 정보를 hitinfo에서 받아온다
        {
            return true;

        }
        return false;

    }

    public void HandChange(Hand _hand)
    {
        if (WeaponMgr.currentWeapon != null)
            WeaponMgr.currentWeapon.gameObject.SetActive(false);

        currentHand = _hand;
        WeaponMgr.currentWeapon = currentHand.GetComponent<Transform>();
        WeaponMgr.currentWeaponAnimator = R_anim;
        WeaponMgr.currentWeaponAnimator = L_anim;

        //currentHand.transform.localPosition = Vector3.zero; 
        currentHand.transform.localPosition = new Vector3(0.1078968f, -1.337683f, 0.6615391f);


        currentHand.gameObject.SetActive(true);
        isActivate = true;
    }
}

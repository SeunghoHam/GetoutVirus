using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCtr : MonoBehaviour
{
    // 활성화 여부
    public static bool isActivate = false;
    //public static bool isActivate = true;
    [SerializeField]
    private Sword currentSword;
    [SerializeField]
    private Crosshair crosshair;

    // 원래 위치
    private Vector3 orignPos;


    // 현재 위치
    private Vector3 currentPos;


    private float currentAttackRate;

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    //공격중?
    private bool isAttack = false;
    private bool isSwing = false;

    [SerializeField]
    private Animator SwordArm_L_anim;
    [SerializeField]
    private Animator SwordArm_R_anim;

    private void Start()
    {
        WeaponMgr.currentWeapon = currentSword.GetComponent<Transform>();
        WeaponMgr.currentWeaponAnimator = SwordArm_L_anim;
        WeaponMgr.currentWeaponAnimator = SwordArm_R_anim;
    }
    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }

    }
    public void WalkingAnimation(bool _flag)
    {
        if (isActivate)
        {
            SwordArm_L_anim.SetBool("Walking", _flag);
            SwordArm_R_anim.SetBool("Walking", _flag);
        }
 
    }

    public void RunningAnimation(bool _flag)
    {
        if (isActivate)
        {
            SwordArm_L_anim.SetBool("Running", _flag);
            SwordArm_R_anim.SetBool("Running", _flag);
        }
       
    }

    void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true; // 중복실행 방지

        SwordArm_R_anim.SetTrigger("Attack"); // 애니메이션 실행
        //currentHand.Crosshair_anim.SetTrigger("Interaction"); // 애니메이션 실행 


        yield return new WaitForSeconds(currentSword.AttackDelayA); // 약간의 딜레이
        isSwing = true;
        // 공격 활성화 시점

        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(currentSword.AttackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentSword.AttackDelay - currentSword.AttackDelayA - currentSword.AttackDelayB);
        isAttack = false;
    }
    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                // 충돌 했음
                //isSwing = false; // 적중 1번만

                if (hitInfo.transform.tag == "NPC")
                    hitInfo.transform.GetComponent<Student>().Damage(1, this.transform.position);
                if (hitInfo.transform.tag == "BOSS")
                    hitInfo.transform.GetComponent<Boss>().Damage(1);



                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
    bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentSword.Range, layerMask))// out hitinfo = 충돌체가 있다면 충돌체의 정보를 hitinfo에서 받아온다
        {
            return true;
        }
        return false;

    }

    public void SwordChange(Sword _sword)
    {
        if (WeaponMgr.currentWeapon != null)
            WeaponMgr.currentWeapon.gameObject.SetActive(false);

        currentSword = _sword;
        WeaponMgr.currentWeapon = currentSword.GetComponent<Transform>();
        WeaponMgr.currentWeaponAnimator = SwordArm_R_anim;
        WeaponMgr.currentWeaponAnimator = SwordArm_L_anim;

        currentSword.transform.localPosition = new Vector3(0.1078959f, -1.337682f, 0.6650016f);



        currentSword.gameObject.SetActive(true);

        isActivate = true;

    }
 
}

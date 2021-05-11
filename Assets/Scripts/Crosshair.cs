using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator Crosshair_anim;

    public void WalkingAnimation(bool _flag)
    {
        //WeaponMgr.currentWeaponAnimator.SetBool("Walking", _flag);
        Crosshair_anim.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        //WeaponMgr.currentWeaponAnimator.SetBool("Running", _flag);
        Crosshair_anim.SetBool("Running", _flag);
    }

}
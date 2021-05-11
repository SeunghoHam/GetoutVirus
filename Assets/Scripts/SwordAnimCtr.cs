using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimCtr : MonoBehaviour
{
    [SerializeField]
    private Animator SwordArm_L_anim;
    [SerializeField]
    private Animator SwordArm_R_anim;

    public void WalkingAnimation(bool _flag)
    {

        SwordArm_L_anim.SetBool("Walking", _flag);
        SwordArm_R_anim.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        SwordArm_L_anim.SetBool("Running", _flag);
        SwordArm_R_anim.SetBool("Running", _flag);
    }
}

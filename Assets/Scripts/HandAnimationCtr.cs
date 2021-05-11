using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationCtr : MonoBehaviour
{

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
}
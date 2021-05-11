using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject theboss;
    [SerializeField] private int hp;

    private bool isDead = false;
    
    [SerializeField] private SphereCollider BossCol;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))    
        {
            theboss.SetActive(true);
        }


        if (isDead)
        {
            
             LoadingSceneController.LoadScene("EndScene");
        }
    }
    public void Damage(int _dmg)
    {
        if (!isDead)
        {
            hp -= _dmg;
            Debug.Log("대미지 입힘");
            if (hp <= 0)
            {
                Dead();
                return;
            }

        }

    }
    void Dead()
    {
        new WaitForSeconds(3f);
        isDead = true;
        Debug.Log("보스 주금");
        Destroy(theboss, 2f);
    }

    
}

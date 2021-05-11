using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Student : MonoBehaviour
{
    [SerializeField] private string studentName; // 학생의 이름
    [SerializeField] private int hp; // 학생의 체력

    [SerializeField] private float walkSpeed; // 걷기 스피드
    [SerializeField] private float runSpeed; // 뛰기 스피드
    private float applySpeed;

    protected Vector3 direction; // 방향

    // 상태변수
    private bool isAction; // 행동중인지 아닌지 판별
    private bool isWalking; // 걷는지 안 걷는지 판별
    protected bool isRunning; // 뛰는지 판별
    protected bool isDead; // 죽었는지 판별

    [SerializeField] private float waitTime; // 대기 시간
    [SerializeField] private float walkTime; // 걷기 시간
    [SerializeField] private float runTime; // 뛰기 시간
    [SerializeField] private float putTime; // 버리고 대기하는 시간
    private float currentTime;

    // 필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;

    [SerializeField] private Transform go_CanPrefab;
    [SerializeField] private Transform go_PlasticPrefab;
    [SerializeField] private Transform go_PaperPrefab;
    //[SerializeField] private Transform g

    
    private void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }


    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime(); // 시간 경과
        }
    }

    void Move()
    {
        if (isWalking || isRunning)
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }

    void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }
    void ReSet()
    {
        isWalking = false; isRunning = false; isAction = true;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking); anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }
    private void RandomAction()
    {
        int _random = Random.Range(0,8);

        if (_random == 0)
            Wait();
        else if (_random == 1)
            PutCan();
        else if (_random == 2)
            PutPlastic();
        else if (_random == 3)
            PutPaper();
        else if (_random == 4)
            TryWalk();
        else if (_random == 5)
            TryWalk();
        else if (_random == 6)
            TryWalk();
        else if (_random == 7)
            TryWalk();

    }

    void Wait()
    {
        currentTime = waitTime;
        //Debug.Log("대기");

    }
    void PutCan()
    {

        currentTime = putTime; // 버린 후 대기시간 작용하게
        //Debug.Log("캔 버리기");
        if (putTime > 0)
        {
            Instantiate(go_CanPrefab,new Vector3(transform.position.x, transform.position.y, transform.position.z -2f), Quaternion.identity);
        }

    }
    void PutPlastic()
    {
        currentTime = putTime; // 버린 후 대기시간 작용하게
       // Debug.Log("플라스틱 버리기");
        if (putTime > 0)
        {
            Instantiate(go_PlasticPrefab,new Vector3(transform.position.x, transform.position.y, transform.position.z -2f), Quaternion.identity);
        }
    }

    void PutPaper()
    {
        currentTime = putTime;
       // Debug.Log("종이 버리기");
        if (putTime > 0)
        {
            Instantiate(go_PaperPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f), Quaternion.identity);
        }
    }
    void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
       // Debug.Log("걷기");
    }
    void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;
        //Debug.Log("뛰는행동 시작");
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
           // Debug.Log("대미지 입힘");
            if (hp <= 0)
            {
                Dead();
                return;
            }

            Run(_targetPos);

        }

    }
    void Dead()
    {
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
        Destroy(gameObject, 6f);
    }
}

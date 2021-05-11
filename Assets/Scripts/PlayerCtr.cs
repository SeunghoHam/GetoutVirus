using UnityEngine;

public class PlayerCtr : MonoBehaviour
{
    // 스피드 조정 변수
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private float applySpeed;

    // 상태변수
    private bool isWalk = false;
    private bool isRun = false;


    // 움직임 체크 변수
    private Vector3 lastPos;

    // 마우스(카메라) 민감도
    [SerializeField] private float lookSensitivity;

    // 카메라 한계
    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    // 필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private HandCtr theHandCtr;
    private SwordCtr theSwordCtr;
    private Crosshair theCrosshair;
    private StatusCtr theStatusCtr;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        theHandCtr = FindObjectOfType<HandCtr>();
        theSwordCtr = FindObjectOfType<SwordCtr>();
       
        theCrosshair = FindObjectOfType<Crosshair>();
        theStatusCtr = FindObjectOfType<StatusCtr>();


        // 초기화
        applySpeed = walkSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCheck();
        if (GameMgr.canPlayerMove)
        {
            TryRun();
            Move();
            if (!Inventory.inventoryActivated)
            {
                CamRotation();
            }
        }

    }

    void Can_PickUP()
    {
        //theStatusCtr.
    }
    // 움직임
    void Move()
    {
        float _MoveDirX = Input.GetAxisRaw("Horizontal");
        float _MoveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _MoveDirX;
        Vector3 _moveVertical = transform.forward * _MoveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    /*void MoveCheck()
    {
        if (!isRun)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;

            theCrosshair.WalkingAnimation(isWalk);
            theHandCtr.WalkingAnimation(isWalk);
            theSwordCtr.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
      


    }*/
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
      
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            RunningCancel();
        }

    }

    void Running()
    {
        isRun = true;
        theHandCtr.RunningAnimation(isRun);
        theSwordCtr.RunningAnimation(isRun);
        theCrosshair.RunningAnimation(isRun);
        applySpeed = runSpeed;
    }
    void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        theHandCtr.RunningAnimation(isRun);
        theSwordCtr.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }

    void CamRotation()
    {
        CameraRotation();
        CharacterRotation();
    }
    // 상하 카메라 회전
    void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX; // 마우스 반전
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // 카메라 시점 가두기

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    // 좌우 카메라 회전
    void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // MoveRotation = Quaternion
    }


}

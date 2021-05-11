using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    // 기존 위치
    private Vector3 orignPos;

    // 현재 위치
    private Vector3 currentPos;
    
    // sway 한계
    [SerializeField]
    private Vector3 limitPos;

    // 부드러운 움직임 정도
    [SerializeField]
    private Vector3 smoothSway;
    // Start is called before the first frame update
    void Start()
    {
        orignPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Inventory.inventoryActivated)
        TrySway();
    }

    void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            Swaying();
        }
        else
            BacktoOriginPos();
    }

    void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");

        currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limitPos.x, limitPos.x),
                       Mathf.Lerp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -limitPos.y, limitPos.y),
                                    orignPos.z);

        transform.localPosition = currentPos;
    }

    void BacktoOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, orignPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewdistance;
    [SerializeField] private LayerMask targetMask;

    private Boss theBoss;

    void Start()
    {
        theBoss = GetComponent<Boss>();
    }
    void Update()
    {
        View();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad)); // y값도 비슷하게 맞춰줘야 플레이어 깔끔하게 볼듯
    }
    void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position,  _rightBoundary, Color.red);


        Collider[] _target = Physics.OverlapSphere(transform.position, viewdistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTF = _target[i].transform;
            if (_targetTF.name == "Player") 
            {
                Vector3 _direction = (_targetTF.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position, _direction, out _hit, viewdistance))
                    {
                        if (_hit.transform.tag == "Player")
                        {
                            Debug.Log("플레이어가 시야 내에 있다");
                            Debug.DrawRay(transform.position, _direction, Color.yellow);
                            // boss의 기술 사용 여기서
                            //thepig.run(_hit.transform.poition); ex
                        }
             
                    }
                }
            }
        }
    }
}

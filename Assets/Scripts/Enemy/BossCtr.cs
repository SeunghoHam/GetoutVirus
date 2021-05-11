using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtr : MonoBehaviour
{
    [SerializeField]
    private GameObject go_BossObject;
    private Vector3 originPos;

    [SerializeField]
    private Transform lookingObject;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime * 5f;
        //Scaling(new Vector3(Mathf.Cos(timer) + 2f, Mathf.Cos(timer) +2f));
        LookGameObject(lookingObject);
        RotateControlForward(new Vector3(Mathf.Cos(timer), 0f, Mathf.Sin(timer)));
        //RotateGameObject(new Vector3(0f, (Mathf.Cos(timer)* 0.5f + 0.5f) * 360f, 0f));
        //LookPosition(new Vector3(Mathf.Cos(timer), Mathf.Sin(timer), Mathf.Sin(timer))); 
        //LookPosition(new Vector3(Mathf.Cos(timer), 0f, Mathf.Sin(timer))); //원본
        //LookPosition(new Vector3(Mathf.Cos(timer), Mathf.Sin(timer), 0f));
        //LookPosition(new Vector3(Mathf.Cos(timer), Mathf.Cos(timer), 0f));
    }
    public void Scaling(Vector3 newScale)
    {
        transform.localScale = newScale;
    }
    public void LookGameObject(Transform lookObj)
    {
        transform.LookAt(lookObj);
    }
    public void LookPosition(Vector3 pos)
    {
        transform.LookAt(pos);
    }
    public void RotateControlForward(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
    void RotateGameObject(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
}

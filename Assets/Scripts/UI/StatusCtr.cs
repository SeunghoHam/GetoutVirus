using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusCtr : MonoBehaviour
{
    [SerializeField] private int Can_currnet;
    private int Can_max;
    [SerializeField] private int Plastic_currnet;
    private int Plastic_max;
    
    // 최대 획득량 
    // Start is called before the first frame update

    [SerializeField] Slider slider_CancurrentGet;
    [SerializeField] Slider slider_PlasticcurrentGet;
    [SerializeField] Text txt_Cancurrent_Get;
    [SerializeField] Text txt_Plasticcurrent_Get;

    private const int CAN = 0, PLASTIC = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUpCan()
    {
        // 아이템을 주웠을때
    }
}

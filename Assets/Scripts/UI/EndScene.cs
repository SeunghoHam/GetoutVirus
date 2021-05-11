using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    
    public void ClickExit()
    {
        
        Debug.Log("게임종료");
        Application.Quit();
    }
}

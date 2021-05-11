using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static bool canPlayerMove = true;
    public static bool isOpenInventory = false;
    public static bool isPause = false;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    
    void Update()
    {
        if (isOpenInventory ||  isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            canPlayerMove = false;
        }
        else
        {
            canPlayerMove = true;
        }
           
    }

}

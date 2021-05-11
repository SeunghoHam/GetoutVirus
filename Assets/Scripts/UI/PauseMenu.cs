using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
       
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameMgr.isPause)
                CallMenu();
            else
                CloseMenu();
        }
        
        
    }
    void CallMenu()
    {
        GameMgr.isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void CloseMenu()
    {
        GameMgr.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickStart()
    {
        Debug.Log("계속하기 누름");
    }
    public void ClickExit()
    {
        Debug.Log("나가기 누름");
        Application.Quit();
    }
}

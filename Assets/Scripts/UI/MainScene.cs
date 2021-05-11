using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{

    public string sceneName = "InGameScene";

   private void Awake() 
   {
        Screen.SetResolution(1280, 720, false);
   }
    public void ClickStart()
    {
        LoadingSceneController.LoadScene(sceneName);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}

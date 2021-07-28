using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void loadscene1(){
        SceneManager.LoadScene("Scene1");
    }
    public void loadscene2(){
        SceneManager.LoadScene("Scene2");
    }
    public void loadscene3(){
        SceneManager.LoadScene("Scene3");
    }
    public void backtomainmenu(){
        SceneManager.LoadScene("StartingScene");
    }
}

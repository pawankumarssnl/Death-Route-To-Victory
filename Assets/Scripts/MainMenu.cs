using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton(){
        Application.Quit();
    }
    public void StartGame(){
        SceneManager.LoadScene("LevelSelector");
    }
    public void helpbutton(){
        SceneManager.LoadScene("Helpscene");
    }
    public void menubutton(){
        SceneManager.LoadScene("Settings");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Functions : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void quitgame()
    {
        Debug.Log("Çýktýn");
        Application.Quit();
    }
    //Settings Menu
    public void setFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    


}

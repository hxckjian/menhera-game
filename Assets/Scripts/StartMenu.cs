using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Called when Start Button is clicked and load the game scene
    public void OnStartClick()
    {
        SceneManager.LoadScene("LivingroomCutscene");
    }

    public void OnTestClick()
    {
        FindFirstObjectByType<SceneTransitionManager>().LoadSceneAndDestroy("SampleScene");
    }
      
    public void OnTutorialClick()
    {
        FindFirstObjectByType<SceneTransitionManager>().LoadSceneAndDestroy("TutorialScene");
    }
    
    // Called when Exit Button is clicked and exit application
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

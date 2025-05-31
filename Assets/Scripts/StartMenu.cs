using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("LivingroomCutscene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnExitClick()
    {
        Debug.Log("Exit button clicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

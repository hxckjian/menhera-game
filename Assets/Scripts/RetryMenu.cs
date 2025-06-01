using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    //Called when retry button is clicked, loads the gameplay scene
    public void OnRetryClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Called when exit button is clicked, return to start menu
    public void OnExitClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}

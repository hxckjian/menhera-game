using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnExitClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnJumpscareEnd()
    {
        SceneManager.LoadScene("RetryScene"); // replace with your actual scene name
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareEnd : MonoBehaviour
{
    public void OnJumpscareEnd()
    {
        SceneManager.LoadScene("RetryScene");
    }
}

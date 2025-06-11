using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private MonoBehaviour[] gameplayScripts;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetGameplayEnabled(bool enabled)
    {
        foreach (var script in gameplayScripts)
        {
            if (script != null)
                script.enabled = enabled;
        }
    }
}

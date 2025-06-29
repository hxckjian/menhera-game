using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance { get; private set; }

    [SerializeField] private MonoBehaviour[] gameplayScripts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // Prevent duplicate singleton instances
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetGameplayEnabled(bool enabled)
    {
        foreach (var script in gameplayScripts)
        {
            if (script != null)
            {
                script.enabled = enabled;
            }
        }
    }
}

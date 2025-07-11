using System.Collections;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    [Header("Yandere Settings")]
    [SerializeField] private GameObject yandere;

    [Header("Delay Settings")]
    [SerializeField] private float yandereAppearDelay = 15f;

    [Header("UI")]
    [SerializeField] private DebugTimerDisplay debugTimer;

    private void Start()
    {
        // Start debug countdown UI
        if (debugTimer != null)
        {
            debugTimer.StartCountdown(yandereAppearDelay);
        }

        if (yandere != null)
        {
            yandere.SetActive(false);
            StartCoroutine(DelayYandereSpawn());
        }
    }

    private IEnumerator DelayYandereSpawn()
    {
        yield return new WaitForSeconds(yandereAppearDelay);
        yandere.SetActive(true);

        // Optional: start chasing behavior
        var ai = yandere.GetComponent<TestYandereAI>();
        if (ai != null)
        {
            ai.BeginChase();
        }
    }
}

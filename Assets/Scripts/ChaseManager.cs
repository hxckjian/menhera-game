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

    [Header("LightManager")]
    [SerializeField] private LightFlickerManager lightManager;

    [Header("Timer End Audio")]
    [SerializeField] private AudioSource timerEndAudio;

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

        lightManager?.StartFlickerRoutine(yandereAppearDelay);
    }

    private IEnumerator DelayYandereSpawn()
    {
        yield return new WaitForSeconds(yandereAppearDelay);

        timerEndAudio?.Play();

        yandere.SetActive(true);

        lightManager?.TurnLightRed();

        // Optional: start chasing behavior
        var ai = yandere.GetComponent<TestYandereAI>();
        if (ai != null)
        {
            ai.BeginChase();
        }
    }
}

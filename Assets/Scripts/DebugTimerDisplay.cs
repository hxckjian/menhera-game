using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugTimerDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text timerText;

    [Header("Debug Settings")]
    [SerializeField] private bool showDebugTimer = true;

    private float elapsedTime = 0f;
    private bool isRunning = true;

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(showDebugTimer);
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        if (showDebugTimer && timerText != null)
        {
            timerText.text = $"Timer: {elapsedTime:F1}s";
        }
    }

    private void StopTimer()
    {
        isRunning = false;
    }
}

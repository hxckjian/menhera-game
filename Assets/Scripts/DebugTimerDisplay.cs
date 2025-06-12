using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugTimerDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text timerText;

    [Header("Debug Settings")]
    [SerializeField] private bool showDebugTimer = true;

    private float timeLeft;
    private bool isRunning = false;

    public void StartCountdown(float delay)
    {
        timeLeft = delay;
        isRunning = true;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(showDebugTimer);
        }
    }

    private void Update()
    {
        if (!isRunning || !showDebugTimer || timerText == null) return;

        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timeLeft);
            int hundredths = Mathf.FloorToInt((timeLeft - seconds) * 100);
            timerText.text = $"{seconds}:{hundredths:00}";
        }
        else
        {
            timerText.text = "0:00";
            isRunning = false;

            //Disable playerinteraction
            PlayerInteraction playerInteraction = FindFirstObjectByType<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.SetInteractEnabled(false);
            }

            Invoke(nameof(HideText), 0f);
        }
    }

    private void HideText()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }
}

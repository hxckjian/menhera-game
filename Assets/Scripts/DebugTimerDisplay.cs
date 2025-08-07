using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugTimerDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject timerPanel;
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
        // Toggle visibility with F3 for development purposes
        if (Input.GetKeyDown(KeyCode.F3))
        {
            showDebugTimer = !showDebugTimer;

            if (timerPanel != null)
            {
                timerPanel.SetActive(showDebugTimer);
            }

            Debug.Log($"[DebugTimerDisplay] showDebugTimer toggled to: {showDebugTimer}");
        }

        if (!isRunning || timerText == null) 
        {
            return;
        }

        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timeLeft);
            int hundredths = Mathf.FloorToInt((timeLeft - seconds) * 100);
            timerText.text = $"{seconds}:{hundredths:00}";
        }
        else
        {
            timerText.text = "----";
            isRunning = false;

            //Disable playerinteraction with area or object when timer over
            DisablePlayerInteraction();

            // Hide Text when timer is 0 but not needed for now
            // Invoke(nameof(HideText), 0f);
        }
    }

    private void DisablePlayerInteraction()
    {
        PlayerInteraction playerInteraction = FindFirstObjectByType<PlayerInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.SetInteractEnabled(false);
            playerInteraction.DisableInteractionPopup();
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

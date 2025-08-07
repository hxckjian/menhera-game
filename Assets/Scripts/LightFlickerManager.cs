using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickerManager : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color flickerColor = new Color(1f, 0.8f, 0.8f);
    [SerializeField] private Color redAlertColor = Color.red;

    [Header("Light Flicker Audio")]
    [SerializeField] private AudioSource lightAudio;

    private Color originalColor;

    private void Awake()
    {
        if (globalLight != null)
            originalColor = globalLight.color;
    }

    public void StartFlickerRoutine(float totalDelay)
    {
        StartCoroutine(FlickerSequence(totalDelay));
    }

    private IEnumerator FlickerSequence(float delay)
    {
        yield return new WaitForSeconds(delay - 10f);
        yield return FlickerOnce(0.15f);

        yield return new WaitForSeconds(5f); // 5s left
        yield return FlickerOnce(0.1f);
        yield return new WaitForSeconds(0.4f);
        yield return FlickerOnce(0.05f);
        yield return new WaitForSeconds(0.3f);
        yield return FlickerOnce(0.05f);
    }

    private IEnumerator FlickerOnce(float duration)
    {
        if (globalLight != null)
        {
            PlayFlickerSound();
            globalLight.intensity = 0.1f; // Almost dark
            yield return new WaitForSeconds(duration);
            globalLight.intensity = 1.7f; // Back to normal brightness
        }
    }

    public void TurnLightRed()
    {
        globalLight.color = redAlertColor;
    }

    private void PlayFlickerSound()
    {
        if (lightAudio != null)
        {
            lightAudio.PlayOneShot(lightAudio.clip);
        }
    }
}

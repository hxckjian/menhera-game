using UnityEngine;
using UnityEngine.Audio;

public class VolumeInitializer : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    void Start()
    {
        float master = PlayerPrefs.GetFloat("Master Volume", 1.0f); // default 100%
        float bgm = PlayerPrefs.GetFloat("BGM", 1.0f);
        float se = PlayerPrefs.GetFloat("SE", 1.0f);

        mixer.SetFloat("Master Volume", Mathf.Log10(Mathf.Clamp(master, 0.0001f, 1f)) * 20);
        mixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(bgm, 0.0001f, 1f)) * 20);
        mixer.SetFloat("SE", Mathf.Log10(Mathf.Clamp(se, 0.0001f, 1f)) * 20);
    }
}

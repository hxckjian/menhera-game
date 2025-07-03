using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace MainScripts
{
    public enum DebugLanguageOption
    {
        None,
        EN,
        JA
    }

    [ExecuteAlways]
    public class LanguageToggle: MonoBehaviour
    {
        [Header("Debug Only: Set test language in Inspector")]
        [SerializeField] private DebugLanguageOption debugLanguage = DebugLanguageOption.JA;

        private DebugLanguageOption _lastLanguage = DebugLanguageOption.JA;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                ApplyLanguage(debugLanguage);
                _lastLanguage = debugLanguage;
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying && debugLanguage != _lastLanguage)
            {
                ApplyLanguage(debugLanguage);
                _lastLanguage = debugLanguage;
            }
        }

        private void ApplyLanguage(DebugLanguageOption option)
        {
            switch (option)
            {
                case DebugLanguageOption.EN:
                    SetLocale("en");
                    break;
                case DebugLanguageOption.JA:
                    SetLocale("ja");
                    break;
                case DebugLanguageOption.None:
                    Debug.Log("Testing Disabled. Language set to: None (no override applied)");
                    break;
            }
        }

        private void SetLocale(string code)
        {
            var locale = LocalizationSettings.AvailableLocales.GetLocale(code);
            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($"Language set to: {locale.Identifier.Code}");
            }
            else
            {
                Debug.LogWarning($"Locale not found: {code}");
            }
        }
    }
}

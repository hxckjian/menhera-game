using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class FontSizeSwitcher : MonoBehaviour
{
    [SerializeField] private float englishFontSize = 140f;
    [SerializeField] private float japaneseFontSize = 120f;

    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        ApplyFontSize();
    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void OnLocaleChanged(UnityEngine.Localization.Locale locale)
    {
        ApplyFontSize();
    }

    private void ApplyFontSize()
    {
        string code = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (code.StartsWith("ja"))
        {
            tmpText.fontSize = japaneseFontSize;
        }
        else
        {
            tmpText.fontSize = englishFontSize;
        }
    }
}

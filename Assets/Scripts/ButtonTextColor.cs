using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI tmpText;
    public Color normalColor = new Color(0.5f, 0.5f, 0.5f); // Dark grey
    public Color highlightedColor = Color.white;

    private void Awake()
    {
        if (tmpText != null)
            tmpText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpText.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.color = EventSystem.current.currentSelectedGameObject == gameObject ? highlightedColor : normalColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        tmpText.color = highlightedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        tmpText.color = normalColor;
    }
}

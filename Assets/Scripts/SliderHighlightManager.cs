using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SliderHighlightManager : MonoBehaviour
{
    [SerializeField] private List<SliderHighlightLink> sliderLinks;

    void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        foreach (var link in sliderLinks)
        {
            bool isSelected = (selected == link.gameObject);
            link.SetHighlightActive(isSelected);
        }
    }
}

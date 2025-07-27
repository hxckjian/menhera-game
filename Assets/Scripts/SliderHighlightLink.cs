using UnityEngine;
using UnityEngine.UI;

public class SliderHighlightLink : MonoBehaviour
{
    [SerializeField] private GameObject highlightImage; 

    public void SetHighlightActive(bool active)
    {
        if (highlightImage != null)
            highlightImage.SetActive(active);
    }
}

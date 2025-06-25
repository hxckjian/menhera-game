using UnityEngine;

public class InteractionPopupUI : MonoBehaviour
{
    [SerializeField] private Animator popupAnimator;

    private bool isPopupShown = false;
    private bool disableAnimation = false;

    public void ShowPopup()
    {
        if (!disableAnimation)
        {
            popupAnimator.ResetTrigger("Hide");
            popupAnimator.SetTrigger("Show");
            isPopupShown = true;
        }
    }

    public void DisablePopup()
    {
        // If popup is shown while timer went below 0:00
        // Hide the popup then disable Animation 
        if (CheckIfShown())
        {
            HidePopup();
        }
        DisableAnimation();
    }

    public void HidePopup()
    {
        if (!disableAnimation)
        {
            popupAnimator.ResetTrigger("Show");
            popupAnimator.SetTrigger("Hide");
            isPopupShown = false;
        }
    }

    public void ResetShow() 
    {
        popupAnimator.ResetTrigger("Show");
    }

    public bool CheckIfShown()
    {
        return isPopupShown;
    }

    private void DisableAnimation()
    {
        disableAnimation = true;
    }
}

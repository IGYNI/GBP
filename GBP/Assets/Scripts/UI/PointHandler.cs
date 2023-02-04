
using UnityEngine;
using UnityEngine.EventSystems;

public class PointHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        AudioManager.instance.InitializeMenuButtonHandler();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //AudioManager.instance.InitializeMenuButtonClick();
    }
}

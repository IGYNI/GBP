
using UnityEngine;
using UnityEngine.EventSystems;

public class PointHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        AudioManager.instance.InitializeMenuButtonHandler();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

}

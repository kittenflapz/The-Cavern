using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Animator doorAnimator;
    bool isOpen;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOpen)
        {
            doorAnimator.SetTrigger("OpenDoor");
            isOpen = true;
        }
    }
}

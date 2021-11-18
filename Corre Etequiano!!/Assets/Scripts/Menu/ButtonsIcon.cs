using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject Icon;

    private void Awake()
    {
        Icon = transform.GetChild(1).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Icon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Icon.SetActive(false);
    }
}

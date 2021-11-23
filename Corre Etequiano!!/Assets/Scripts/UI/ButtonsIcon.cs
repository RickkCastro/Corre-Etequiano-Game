using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Script para botoes mostrarem um icon do lado quando o clik ou o mouse estiver em cima

    private GameObject Icon; //Icon

    private void Awake()
    {
        Icon = transform.GetChild(1).gameObject; //Pegar icon
    }

    public void OnPointerEnter(PointerEventData eventData) //Se o ponteiro estiver em cima
    {
        Icon.SetActive(true); //Ativar icon
    }

    public void OnPointerExit(PointerEventData eventData) //Se o ponteiro n estiver em cima
    {
        Icon.SetActive(false); //Desativar icon
    }
}

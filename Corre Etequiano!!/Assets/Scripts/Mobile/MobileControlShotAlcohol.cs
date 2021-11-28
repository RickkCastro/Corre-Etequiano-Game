using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControlShotAlcohol : MonoBehaviour, IPointerClickHandler
{
    //Script para controlar o tiro no celular

    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // player
    }

    public void OnPointerClick(PointerEventData eventData) //quando cliar
    {
        player.ShotAlcohol();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControlJump : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //Script para controlar o pulo no celular

    private Player player;
    bool Holding;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //player
    }

    public void OnPointerUp(PointerEventData eventData) //quando soltar a tela
    {
        Holding = false;
    }

    public void OnPointerDown(PointerEventData eventData) //quando cliar na tela
    {
        Holding = true;
    }

    private void Update() //a todo momento
    {
        if (PlayerPrefs.GetInt("IsMobile", 1) == 1)
        {
            if (Holding)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //player
                player.Jump();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControlJump : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Player player;
    bool Holding;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Holding = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Holding = true;
    }

    private void Update()
    {
        if (Holding)
            player.Jump();
    }
}

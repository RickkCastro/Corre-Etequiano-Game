using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControlShotAlcohol : MonoBehaviour, IPointerClickHandler
{
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.ShotAlcohol();
    }
}

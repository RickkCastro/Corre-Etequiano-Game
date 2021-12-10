using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnScreen : MonoBehaviour
{
    private TextMeshProUGUI CountdownTxt;

    private void Start()
    {
        CountdownTxt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        CountdownTxt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        CountdownTxt.text = "3";
        yield return new WaitForSeconds(1f);
        CountdownTxt.text = "2";
        yield return new WaitForSeconds(1f);
        CountdownTxt.text = "1";
        yield return new WaitForSeconds(1f);

        GameController.instance.IsPaused = false; //Despausar jogo
        gameObject.SetActive(false);

        CountdownTxt.text = "3s";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnScreen : MonoBehaviour
{
    public bool CountdownFinish;
    private TextMeshProUGUI CountdownTxt;

    private void Start()
    {
        CountdownTxt = transform.GetChild(0).GetComponent < TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        CountdownFinish = false;

        CountdownTxt.text = "3s";
        yield return new WaitForSeconds(1f);
        CountdownTxt.text = "2s";
        yield return new WaitForSeconds(1f);
        CountdownTxt.text = "1s";
        yield return new WaitForSeconds(1f);

        CountdownFinish = true;
        gameObject.SetActive(false);

        CountdownTxt.text = "3s";
    }
}

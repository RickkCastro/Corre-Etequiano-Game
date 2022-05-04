using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenaryIntro : MonoBehaviour
{
    [SerializeField]
    float countdownSeconds;
    [SerializeField]
    private GameObject PanelIntro;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Transform bossPositionX;

    private bool irParaPosicao;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(countdownSeconds);
        PanelIntro.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.4f);
        PanelIntro.SetActive(false);
        boss.SetActive(true);
        irParaPosicao = true;
    }

    private void Update()
    {
        if (boss.transform.position.x > bossPositionX.position.x && irParaPosicao)
        {
            boss.transform.position = new Vector2(boss.transform.position.x - GameController.instance.CurrentSpeed * Time.deltaTime, boss.transform.position.y);
            if (boss.transform.position.x < bossPositionX.position.x)
                irParaPosicao = false;
        }
    }
}

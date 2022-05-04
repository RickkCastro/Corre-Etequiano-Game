using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresidenteController : MonoBehaviour
{
    public float Maxlife;
    public float life;

    [SerializeField]
    private List<Sprite> heartsList;
    [SerializeField]
    private SpriteRenderer hearts;

    private Animator animator;

    [SerializeField]
    private UnityEvent DeathEvent;
    [SerializeField]
    private UnityEvent DamageEvent;

    private PresidenteAttacks presidenteAttacks;

    private void Start()
    {
        animator = GetComponent<Animator>();
        life = Maxlife;
        presidenteAttacks = GetComponent<PresidenteAttacks>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        lifeUIModify();
        DamageEvent.Invoke();

        StartCoroutine(DamageAnim());

        if (life <= 0)
            Death();
        else if(life <= Maxlife/2)
            Stage2();
    }

    private void lifeUIModify()
    {
        int numHeart = (int)life;
        
        if(numHeart < 0)
            numHeart = 0;

        hearts.sprite = heartsList[numHeart];
    }

    private IEnumerator DamageAnim() //anim de dano
    {
        for (int i = 0; i < 3; i++)
        {
            float time = 0.1f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(time);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(time);
        }
    }

    private void Stage2()
    {
        animator.SetBool("Stage 2", true);
        presidenteAttacks.currentBullet = presidenteAttacks.bulletStage2;
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        StartCoroutine(DeathCount(1.5f));
    }

    private IEnumerator DeathCount(float seconds)
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(seconds);
        DeathEvent.Invoke();
    }
}

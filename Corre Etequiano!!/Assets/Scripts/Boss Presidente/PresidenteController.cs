using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresidenteController : MonoBehaviour
{
    [SerializeField]
    private float Maxlife;
    private float life;

    [SerializeField]
    private List<Sprite> heartsList;
    [SerializeField]
    private SpriteRenderer hearts;

    private Animator animator;

    [SerializeField]
    private UnityEvent DeathEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        life = Maxlife;
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        lifeUIModify();
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
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        StartCoroutine(DeathCount(1.5f));
    }

    private IEnumerator DeathCount(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeathEvent.Invoke();
    }
}

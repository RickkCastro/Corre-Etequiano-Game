using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VaccineShieldScript : MonoBehaviour
{
    private Transform ShieldHand;
    public float DurantionTime;
    public float time;

    public GameObject TxtShieldTime;

    private void Awake()
    {
        ShieldHand = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(ShieldHand.position.x, ShieldHand.position.y);
    }

    public void ActivateShield()
    {
        StopAllCoroutines();
        StartCoroutine(ShieldDuration());
    }

    public IEnumerator ShieldDuration()
    {
        float time = DurantionTime;
        bool Activate = true;

        TxtShieldTime.SetActive(true);
        TxtShieldTime.GetComponent<TextMeshProUGUI>().text = time + "s";

        while (Activate)
        {
            yield return new WaitForSeconds(1f);
            time--;
            TxtShieldTime.GetComponent<TextMeshProUGUI>().text = time + "s";

            if (time < 1)
            {
                Activate = false;
            }
        }

        TxtShieldTime.SetActive(false);
        gameObject.SetActive(false);
    }
}

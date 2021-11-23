using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VaccineShieldScript : MonoBehaviour
{
    //Script do Escudo de vacina

    private Transform ShieldHand; //Lugar onde o escudo aparece no player
    public float DurantionTime; //Tempo de duracao do escudo

    public GameObject TxtShieldTime; //texto de tempo do escudo

    private void Awake()
    {
        ShieldHand = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1); //Pegar mao do escudo
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Obstacle")) //Caso o escudo colida com algum ostacle
        {
            Destroy(collision.gameObject); //Destruir obstacle
        }
    }

    private void Update() //A todo momento
    {
        transform.position = new Vector2(ShieldHand.position.x, ShieldHand.position.y); //Escudo no Y do player
    }

    public void ActivateShield() //Ativar escudo
    {
        StopAllCoroutines(); //Para todas as corotinas para recomecar
        StartCoroutine(ShieldDuration()); //Comeca escudo
    }

    public IEnumerator ShieldDuration()
    {
        float time = DurantionTime; //Tempo = a tempo de duracao
        bool Activate = true; //Escudo esta ativado

        TxtShieldTime.SetActive(true); //Ativa texto
        TxtShieldTime.GetComponent<TextMeshProUGUI>().text = time + "s"; //Muda texto

        while (Activate) //Se escudo estiver ativo = loop
        {
            yield return new WaitForSeconds(1f); //Esperar 1s
            time--; //Diminuir tempo
            TxtShieldTime.GetComponent<TextMeshProUGUI>().text = time + "s"; //Mudar texto

            if (time < 1) //Se tempo for < 0
            {
                Activate = false; //Desativar escudo
            }
        }

        TxtShieldTime.SetActive(false); //Desativar texto
        gameObject.SetActive(false); //Desativar objeto do escudo
    }
}

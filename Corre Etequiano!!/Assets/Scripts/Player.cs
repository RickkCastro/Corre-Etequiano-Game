using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float JumpForce; //Força do pulo
    public int Life; //Quantidade de vida
    public int AlcoholAmmu; //Quantidade de alcohol do player
    public GameObject Alcohol; //Tiro de Alcohol

    private bool isGrounded = false; //Se o player esta tocando no chao

    private Rigidbody2D RB; 
    private Transform hand; //Mao onde items sao criados na frente do player
    public GameObject VaccineShield;

    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>(); //Pegar ridgbody
        hand = transform.GetChild(0); //Pegar mao
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) //Pular, apertar espaco
        {
            if (isGrounded) //se o player estiver no chao
            {
                isGrounded = false;
                RB.AddForce(Vector2.up * JumpForce); //pulo
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) //Atirar, apertar e
        {
            if(AlcoholAmmu > 0) //Se a municao n for 0
            {
                GameObject ins = Instantiate(Alcohol, hand.position, hand.rotation); //criar tiro
                AlcoholAmmu--; //Diminuir municao
                GameUI.instance.UpdateAlcohol(AlcoholAmmu);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Colisoes
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Com obstaculo
        {
            Destroy(collision.gameObject); //Destruir obstacle
            Life--; //Diminuir vida
            GameUI.instance.UpdateMask(Life);

            if (Life < 1) //Caso a vida seja menor q 1
            {
                Death(); //Morrer
            }
        }

        if (collision.gameObject.CompareTag("Item")) //Com Item
        {
            ItemEffect(collision.gameObject.GetComponent<ItemScript>().ItemType); //Ativar o efeito do item correspondente 
            Destroy(collision.gameObject); //Destruir item
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Colisao com o chao
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; //mudar bool
        }
    }

    private void Death() //Morrer
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ItemEffect(string ItemType) //Efeitos dos itens
    {
        //Debug.Log(ItemType);

        if(ItemType == "LifeMask") //Mascara
        {
            if (Life < 3) //Se a vida for menor q 3
            {
                Life++; //Aumentar vida
                GameUI.instance.UpdateMask(Life);
            }
        }

        if (ItemType == "AlcoholAmmu") //Alcohol
        {
            if (AlcoholAmmu < 3) //Se for menor q 3
            {
                AlcoholAmmu++; //Aumentar alcohol
                GameUI.instance.UpdateAlcohol(AlcoholAmmu);
            }
        }

        if (ItemType == "Vaccine") //Vaccine
        {
            VaccineShield.SetActive(true);
            VaccineShield.GetComponent<VaccineShieldScript>().ActivateShield();
        }
    }
}

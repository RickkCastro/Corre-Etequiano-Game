using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float JumpForce; //Força do pulo
    public int Life; //Quantida de vida
    public int AlcoholAmmu;
    public GameObject Alcohol;

    private bool isGrounded = false; //Se o player esta tocando no chao

    private Rigidbody2D RB;
    private Transform hand;

    private Image MaskUI;
    private Image AlcoholUI;

    // Start is called before the first frame update
    void Awake()
    {
        MaskUI = GameObject.FindGameObjectWithTag("PlayerLife").GetComponent<Image>();
        AlcoholUI = GameObject.FindGameObjectWithTag("PlayerAmmu").GetComponent<Image>();

        MaskUI.sprite = GameUI.instance.ImgMask;

        RB = GetComponent<Rigidbody2D>();
        hand = transform.GetChild(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) //Pular
        {
            if (isGrounded) //se o player estiver no chao
            {
                isGrounded = false;
                RB.AddForce(Vector2.up * JumpForce); //pulo
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) //Atirar
        {
            if(AlcoholAmmu > 0)
            {
                GameObject ins = Instantiate(Alcohol, hand.position, hand.rotation);
                AlcoholAmmu--;
                GameUI.instance.LessAlcohol();
                AlcoholUI.sprite = GameUI.instance.ImgAlcohol;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Com obstaculo
        {
            Destroy(collision.gameObject);
            Life--; //Diminuir vida
            GameUI.instance.LessMask();
            MaskUI.sprite = GameUI.instance.ImgMask;

            if (Life < 1) //Caso a vida seja menor q 1
            {
                Death(); //Morrer
            }
        }

        if (collision.gameObject.CompareTag("Item")) //Com obstaculo
        {
            ItemEffect(collision.gameObject.GetComponent<ItemScript>().ItemType);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Com o chao
        {
            isGrounded = true; //mudar bool
        }
    }

    private void Death() //Morrer
    {
        SceneManager.LoadScene(0);
    }

    private void ItemEffect(string ItemType)
    {
        Debug.Log(ItemType);

        if(ItemType == "LifeMask")
        {
            if (Life < 3)
            {
                Life++;
                GameUI.instance.MoreMask();
                MaskUI.sprite = GameUI.instance.ImgMask;
            }
        }

        if (ItemType == "AlcoholAmmu")
        {
            if (AlcoholAmmu < 3)
            {
                AlcoholAmmu++;
                GameUI.instance.MoreAlcohol();
                AlcoholUI.sprite = GameUI.instance.ImgAlcohol;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float JumpForce; //Força do pulo
    public float Life; //Quantida de vida

    [SerializeField]
    bool isGrounded = false; //Se o player esta tocando no chao

    Rigidbody2D RB;
    private TextMeshProUGUI TxtLife;

    // Start is called before the first frame update
    void Awake()
    {
        TxtLife = GameObject.FindGameObjectWithTag("PlayerLife").GetComponent<TextMeshProUGUI>();
        TxtLife.text = "Masks: " + Life.ToString("00");
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) //Pular
        {
            if (isGrounded) //se o player estiver no chao
            {
                isGrounded = false;
                RB.AddForce(Vector2.up * JumpForce); //pulo
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Colisoes
    {
        if (collision.gameObject.CompareTag("Ground")) //Com o chao
        {
            isGrounded = true; //mudar bool
        }

        if (collision.gameObject.CompareTag("Obstacle")) //Com obstaculo
        {
            Destroy(collision.gameObject);
            Life--; //Diminuir vida
            TxtLife.text = "Masks: " + Life.ToString("00");
            if (Life < 1) //Caso a vida seja menor q 1
            {
                Death(); //Morrer
            }
        }
    }

    private void Death() //Morrer
    {
        SceneManager.LoadScene(0);
    }
}

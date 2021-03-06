using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float JumpForce; //For�a do pulo
    public int Life; //Quantidade de vida
    public int AlcoholAmmu; //Quantidade de alcohol do player
    public GameObject Alcohol; //Tiro de Alcohol
    public GameObject VaccineShield; //Escudo
    public bool immortal; //personagem n perde vida

    [Header("Sounds")]
    public AudioClip sJump;
    public AudioClip sItem;
    public AudioClip sDamage;
    public AudioClip sDeath;
    public AudioClip sAlcoholShot;

    //Privados
    private bool isGrounded = false; //Se o player esta tocando no chao
    private Rigidbody2D RB; //Rigdbody
    private Transform hand; //Mao onde items sao criados na frente do player
    private Animator anim; //Animator

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>(); //Pegar ridgbody
        anim = GetComponent<Animator>(); //Pegar animator

        hand = transform.GetChild(0); //Pegar mao
        VaccineShield = GameObject.FindGameObjectWithTag("VaccineShield");
        VaccineShield.SetActive(false);

        //Pegar variaveis certas
        Life = PlayerPrefs.GetInt("PlayerLife", 3);
        GameUI.instance.UpdateLife(Life); //Atualizar imagem de mascaras

        AlcoholAmmu = PlayerPrefs.GetInt("PlayerAlcohol", 3);
        GameUI.instance.UpdateAlcohol(AlcoholAmmu); 
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) //Pular, apertar espaco
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E)) //Atirar, apertar e
        {
            ShotAlcohol();
        }

        //Pausar anim do player
        if(GameController.instance.IsPaused == true)
        {
            RB.simulated = false;
            anim.speed = 0;
        }
        else
        {
            RB.simulated = true;
            anim.speed = 1;
        }
    }

    public void Jump()
    {
        if (isGrounded) //se o player estiver no chao
        {
            //Som de pulo
            GetComponent<AudioSource>().clip = sJump;
            GetComponent<AudioSource>().Play();

            isGrounded = false;
            RB.AddForce(Vector2.up * JumpForce); //pulo
        }
    }

    public void ShotAlcohol()
    {
        if (AlcoholAmmu > 0 ) //Se a municao n for 0
        {
            //Som do tiro
            GetComponent<AudioSource>().clip = sAlcoholShot;
            GetComponent<AudioSource>().Play();

            GameObject ins = Instantiate(Alcohol, hand.position, hand.rotation); //criar tiro
            ins.transform.parent = this.transform;

            if (!immortal)
            {
                AlcoholAmmu--; //Diminuir municao
                PlayerPrefs.SetInt("PlayerAlcohol", AlcoholAmmu);

                GameUI.instance.UpdateAlcohol(AlcoholAmmu); //Atualizar imagem do alcohol
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Colisoes
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Com obstaculo
        {
            if (!immortal) //Se nao for imortal
            {
                //Som de dano
                GetComponent<AudioSource>().clip = sDamage;
                GetComponent<AudioSource>().Play();

                Life--; //Diminuir vida
                PlayerPrefs.SetInt("PlayerLife", Life);
                GameUI.instance.UpdateLife(Life); //Atualizar imagem das mascaras
                StartCoroutine(DamageAnim()); //Anim de dano

                if (Life < 1) //Caso a vida seja menor q 1
                {
                    Death(); //Morrer
                }
            }

            Destroy(collision.gameObject); //Destruir obstacle
        }

        if (collision.gameObject.CompareTag("Item")) //Com Item
        {
            ItemEffect(collision.gameObject.GetComponent<ItemScript>().ItemType); //Ativar o efeito do item correspondente 
            Destroy(collision.gameObject); //Destruir item
        }
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

    private void OnCollisionEnter2D(Collision2D collision) //Colisao com o chao
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; //mudar bool
        }
    }

    private void Death() //Morrer
    {
        //Som de morrer
        GetComponent<AudioSource>().clip = sDeath;
        GetComponent<AudioSource>().Play();

        GameController.instance.IsDeath = true;

        try
        {
            GameObject.Find("ControlMusic").GetComponent<MusicScript>().BGM.volume = 0;
        }
        catch
        {
            Debug.Log("Erro ao pegar Control Music");
        }

        GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();

        int CountForAd = PlayerPrefs.GetInt("CountForAd", 0) + 1;
        PlayerPrefs.SetInt("CountForAd", CountForAd);

        if(CountForAd >= GameController.instance.MatchesForAd)
        {
            PlayerPrefs.SetInt("CountForAd", 0);
            gameScreens.WatchAdInterstitial();
        }

        //Chama tela de morte
        gameScreens.CallDeathScreen();
    }

    private void ItemEffect(string ItemType) //Efeitos dos itens
    {
        //Debug.Log(ItemType);

        //Som de pegar item
        GetComponent<AudioSource>().clip = sItem;
        GetComponent<AudioSource>().Play();

        if(ItemType == "LifeMask") //Mascara
        {
            if (Life < 3) //Se a vida for menor q 3
            {
                Life++; //Aumentar vida
                PlayerPrefs.SetInt("PlayerLife", Life);
                GameUI.instance.UpdateLife(Life);
            }
        }

        if (ItemType == "AlcoholAmmu") //Alcohol
        {
            if (AlcoholAmmu < 3) //Se for menor q 3
            {
                AlcoholAmmu++; //Aumentar alcohol
                PlayerPrefs.SetInt("PlayerAlcohol", AlcoholAmmu);
                GameUI.instance.UpdateAlcohol(AlcoholAmmu);
            }
        }

        if (ItemType == "Vaccine") //Vaccine
        {
            VaccineShield.SetActive(true); //Ativa escudo
            VaccineShield.GetComponent<VaccineShieldScript>().ActivateShield(); //Chama script do escudo
        }
    }
}

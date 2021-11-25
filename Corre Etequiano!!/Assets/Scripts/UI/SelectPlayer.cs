using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    //Script da tela de selecionar personagem

    [System.Serializable]
    public class Player
    {
        public string Name; //Nome do player
        public string Id;
        public Sprite sprite; //Sprite do player
    }

    public List<Player> Players; //Lista de players

    int CurrentPlayer; //Num do player atual

    public Image PlayerSprite; //Sprite onde são mostrados os players
    public TextMeshProUGUI TxtName; //Texto do nome do player

    public GameObject LoadingScreen;

    public AudioClip SoundSelectPlayer; //Som de selecionar player


    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = 0; //Colocar no player 0

        PlayerSprite.sprite = Players[CurrentPlayer].sprite; //Mudar Sprite
        TxtName.text = Players[CurrentPlayer].Name; //Mudar nome
    }

    public void NextPlayer() //botao de proximo player
    {
        if (CurrentPlayer < Players.Count - 1) //Se o valor n utrapassar o maximo de players
            CurrentPlayer++; //Mudar player
        else
            CurrentPlayer = 0; //Voltar ao primeiro

        PlayerSprite.sprite = Players[CurrentPlayer].sprite; //Mudar sprite
        TxtName.text = Players[CurrentPlayer].Name; //Mudar nome

        //Som de click
        GetComponent<AudioSource>().Play();
        StartCoroutine(ImgAnim()); //Anim de troca de player
    }

    public void BackPlayer() //Botao player anterior
    {
        if (CurrentPlayer > 0) //Se o valor n utrapassar for menor que 0
            CurrentPlayer--; //Mudar player
        else
            CurrentPlayer = Players.Count - 1; //Ir ao ultimo

        PlayerSprite.sprite = Players[CurrentPlayer].sprite; //Mudar sprite
        TxtName.text = Players[CurrentPlayer].Name; //Mudar nome

        //Som de click
        GetComponent<AudioSource>().Play();
        StartCoroutine(ImgAnim()); //Anim de troca de player
    }

    IEnumerator ImgAnim() //Animacao de troca de player
    {
        PlayerSprite.transform.position= new Vector2(PlayerSprite.transform.position.x + 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x - 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x - 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x + 4, PlayerSprite.transform.position.y);
    }

    public void GoToGame() //Ir para o jogo
    {
        StartCoroutine(GoToGameLoad());
    }

    IEnumerator GoToGameLoad()
    {
        LoadingScreen.SetActive(true);

        //Som de selecionar player
        GetComponent<AudioSource>().clip = SoundSelectPlayer;
        GetComponent<AudioSource>().Play();

        //Colocar o id do player no bd
        PlayerPrefs.SetString("PlayerId", Players[CurrentPlayer].Id);

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Scenary1"); //Carregar cenario 1
    }
}

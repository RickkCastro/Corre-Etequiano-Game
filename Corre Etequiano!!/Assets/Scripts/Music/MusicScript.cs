using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    //Script que controla a troca de musica

    public AudioSource BGM; //objeto q toca musica
    public AudioClip SceneMusic; //musica da cena 

    // Start is called before the first frame update
    void Start()
    {
        try //tentar achar objeto
        {
            BGM = GameObject.Find("BGM").GetComponent<AudioSource>(); //Objeto que toca musica de fundo
        }
        catch { //Criar caso n ache
            BGM = Instantiate(Resources.Load<GameObject>("DontDestroy/BGM").GetComponent<AudioSource>());
        }

        if (BGM.clip != SceneMusic) //Se a musica do BGM for diferente da musica da cena
        {
            StartCoroutine(ChangeMusic()); //Mudar musica
        }
    }

    IEnumerator ChangeMusic() //mudar musica
    {
        BGM.volume = 0.5f;
        while (BGM.volume > 0f) //FadeIn da musica (abaixar)
        {
            BGM.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        BGM.clip = SceneMusic; //Mudar musica
        BGM.Play();

        while (BGM.volume < 0.5f) //FadeOut (Aumentar)
        {
            BGM.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RestartMusic() //Reiniciar musica
    {
        BGM.volume = 0.5f;
        BGM.Play();
    }
}

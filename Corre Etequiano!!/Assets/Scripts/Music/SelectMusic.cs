using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : MonoBehaviour
{
    private AudioSource BGM;
    public AudioClip SceneMusic;

    // Start is called before the first frame update
    void Start()
    {
        try //tentar achar objeto
        {
            BGM = GameObject.Find("BGM").GetComponent<AudioSource>(); //Objeto que toca musica de fundo

            if (BGM.clip != SceneMusic) //Se a musica do BGM for diferente da musica da cena
            {
                StartCoroutine(ChangeMusic()); //Mudar musica
            }
        }
        catch { }
    }

    IEnumerator ChangeMusic()
    {
        while (BGM.volume > 0) //FadeIn da musica (abaixar)
        {
            BGM.volume -= 0.08f;
            yield return new WaitForSeconds(0.1f);
        }

        BGM.clip = SceneMusic; //Mudar musica
        BGM.Play();

        while (BGM.volume < 0.5) //FadeOut (Aumentar)
        {
            BGM.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

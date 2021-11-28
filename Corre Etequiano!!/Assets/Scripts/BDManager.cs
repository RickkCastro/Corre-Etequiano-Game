using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDManager : MonoBehaviour
{
    //Script para controlar o banco de dados

    public static BDManager instace;

    // Start is called before the first frame update
    void Start()
    {
        instace = this;

        DontDestroyOnLoad(gameObject); //nao destruir ao carrecar
        ReniciarBd();
    }
    public void ReniciarBd() //Apagar tudo
    {
        string PlayerId = PlayerPrefs.GetString("PlayerId");
        int IsMobile = PlayerPrefs.GetInt("IsMobile");
        int BestTime = PlayerPrefs.GetInt("BestTime");
        int CountForAd = PlayerPrefs.GetInt("CountForAd");

        PlayerPrefs.DeleteAll();

        //Guardar variaveis q nao devem reiniciar
        PlayerPrefs.SetString("PlayerId", PlayerId);
        PlayerPrefs.SetInt("BestTime", BestTime);
        PlayerPrefs.SetInt("IsMobile", IsMobile);
        PlayerPrefs.SetInt("CountForAd", CountForAd);
    }
}

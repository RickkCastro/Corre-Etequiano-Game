using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //Script para cuidar da imagem UI das mascara e do alcool na tela de jogo

    public List<Sprite> lifeSprites; //Lista de sprites da Ui de mascaras
    public List<Sprite> AlcoholSprites; //Lista de sprites da Ui de alcohol

    private Image MaskUI; //UI da mascara
    private Image AlcoholUI; //Ui do Alcohol

    public static GameUI instance; 
    private void Awake()
    {
        instance = this;
        MaskUI = GameObject.FindGameObjectWithTag("PlayerLife").GetComponent<Image>(); //Pegar a imagem de mascaras na UI
        AlcoholUI = GameObject.FindGameObjectWithTag("PlayerAmmu").GetComponent<Image>(); //Pegar a imagem dos alcools na UI

        MaskUI.sprite = lifeSprites[lifeSprites.Count -1]; //Colocar mascara inicial
        AlcoholUI.sprite = AlcoholSprites[AlcoholSprites.Count -1]; //Colocar alcohol inicial
    }

    public void UpdateLife(int life) //Atualizar mascaras
    {
        MaskUI.sprite = lifeSprites[life];
    }

    public void UpdateAlcohol(int AlcolholAmmu) //Atualizar alcool
    {
        AlcoholUI.sprite = AlcoholSprites[AlcolholAmmu];
    }
}

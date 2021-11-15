using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //Codigo para cuidar das imagens de contagem de vida e alccohol

    public List<Sprite> MaskSprites; //Lista de sprites da Ui de mascaras
    public List<Sprite> AlcoholSprites; //Lista de sprites da Ui de alcohol

    private Image MaskUI; //UI da mascara
    private Image AlcoholUI; //Ui do Alcohol

    public static GameUI instance; 
    private void Awake()
    {
        instance = this;
        MaskUI = GameObject.FindGameObjectWithTag("PlayerLife").GetComponent<Image>();
        AlcoholUI = GameObject.FindGameObjectWithTag("PlayerAmmu").GetComponent<Image>();

        MaskUI.sprite = MaskSprites[MaskSprites.Count -1]; //Colocar mascara inicial
        AlcoholUI.sprite = AlcoholSprites[AlcoholSprites.Count -1]; //Colocar alcohol inicial
    }

    public void UpdateMask(int Mask)
    {
        MaskUI.sprite = MaskSprites[Mask];
    }

    public void UpdateAlcohol(int AlcolholAmmu)
    {
        AlcoholUI.sprite = AlcoholSprites[AlcolholAmmu];
    }
}

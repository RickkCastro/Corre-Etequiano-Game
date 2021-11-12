using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public List<Sprite> MaskSprites;
    public List<Sprite> AlcoholSprites;

    public int CurrentMask;
    public int CurrentAlcohol;

    public Sprite ImgMask;
    public Sprite ImgAlcohol;

    public static GameUI instance;
    private void Awake()
    {
        instance = this;
        ImgMask = MaskSprites[CurrentMask];
    }

    public void MoreMask()
    {
        if(CurrentMask > 0) //0, 1, 2
        {
            CurrentMask--;
            ImgMask = MaskSprites[CurrentMask];
        }
    }

    public void LessMask()
    {
        CurrentMask++;
        ImgMask = MaskSprites[CurrentMask];
    }

    public void MoreAlcohol()
    {
        if (CurrentAlcohol > 0) //0, 1, 2
        {
            CurrentAlcohol--;
            ImgAlcohol = AlcoholSprites[CurrentAlcohol];
        }
    }

    public void LessAlcohol()
    {
        CurrentAlcohol++;
        ImgAlcohol = AlcoholSprites[CurrentAlcohol];
    }
}

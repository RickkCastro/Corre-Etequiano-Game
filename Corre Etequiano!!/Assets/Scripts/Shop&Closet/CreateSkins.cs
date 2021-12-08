using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSkins : MonoBehaviour
{
    [System.Serializable]
    public class Skin{
        public string Name;
        public Sprite sprite;
    }

    public List<Skin> SkinsList;

    private GameObject SkinTemplate; //Template dos itens a serem criados
    private GameObject g; //Armazenar obj a serem criados
    public Transform SkinsScrollView; //Local do shop onde são criado os itens

    private void Awake()
    {
        SkinTemplate = SkinsScrollView.GetChild(0).gameObject; //pegar template
        
        int len = SkinsList.Count; //num max de skins
        for(int i = 0; i < len; i++){
            g = Instantiate(SkinTemplate, SkinsScrollView); //Criar skin

            //Nome
            g.transform.GetChild(0).name = SkinsList[i].Name;

            //Sprite
            g.transform.GetChild(0).GetComponent<Image>().sprite = SkinsList[i].sprite;

            //Buttons
            g.transform.GetComponent<Button>().AddEventListener (i, SelectSkin.Instance.OnAvatarClick);

            SelectSkin.Instance.SelectAvatar(1);
        }
        
        Destroy(SkinTemplate);
    }
}

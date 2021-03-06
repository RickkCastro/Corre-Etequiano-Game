using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectSkin : MonoBehaviour
{
    #region Singlton:SelectSkin

	public static SelectSkin Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion


    [SerializeField] Transform AvatarsScrollView;

    int newSelectedIndex, previousSelectedIndex;

	[SerializeField] Color ActiveAvatarColor;
	[SerializeField] Color DefaultAvatarColor;

	[SerializeField] TextMeshProUGUI TxtName;
	[SerializeField] GameObject LoadingScreen;
	[SerializeField] AudioClip SoundSelectPlayer;

	void Start ()
	{
		newSelectedIndex = previousSelectedIndex = 0;
	}

    public void OnAvatarClick (int AvatarIndex)
	{
		SelectAvatar (AvatarIndex);
	}

	public void SelectAvatar (int AvatarIndex)
	{
		previousSelectedIndex = newSelectedIndex;
		newSelectedIndex = AvatarIndex;
		AvatarsScrollView.GetChild (previousSelectedIndex).GetComponent <Image> ().color = DefaultAvatarColor;
		AvatarsScrollView.GetChild (newSelectedIndex).GetComponent <Image> ().color = ActiveAvatarColor;

		GetComponent<AudioSource>().Play();
		TxtName.text = AvatarsScrollView.GetChild(newSelectedIndex).GetChild(0).name;
	}

	public void GoToGame() //Ir para o jogo
    {
		//Resetar valores
        try //tentar achar objeto
        {
            BDManager.instace.ReniciarBd();
        }
        catch
        { //Criar caso n ache
            BDManager bdManager = Instantiate(Resources.Load<GameObject>("DontDestroy/BDManager").GetComponent<BDManager>());
            bdManager.ReniciarBd();
        }
		
        StartCoroutine(GoToGameLoad());
    }

    IEnumerator GoToGameLoad()
    {
        LoadingScreen.SetActive(true);

        //Som de selecionar player
        GetComponent<AudioSource>().clip = SoundSelectPlayer;
        GetComponent<AudioSource>().Play();

        //Colocar o id do player no bd
		string PlayerName = AvatarsScrollView.GetChild(newSelectedIndex).GetChild(0).name;
		Debug.Log(PlayerName);
        PlayerPrefs.SetString("PlayerName", PlayerName);

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Scenary1"); //Carregar cenario 1
    }
}

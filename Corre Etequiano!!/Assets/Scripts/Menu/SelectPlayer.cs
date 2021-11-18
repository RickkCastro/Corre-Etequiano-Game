using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public string Name;
        public Sprite sprite;
    }

    public List<Player> Players;
    int CurrentPlayer;
    public Image PlayerSprite;
    public TextMeshProUGUI TxtName;

    public GameObject TutorialPc;
    public GameObject TutorialMobile;

    public bool isMobile;


    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = 0;
        PlayerSprite.sprite = Players[CurrentPlayer].sprite;
        TxtName.text = Players[CurrentPlayer].Name;
    }

    public void NextPlayer()
    {
        if (CurrentPlayer < Players.Count - 1)
            CurrentPlayer++;
        else
            CurrentPlayer = 0;

        PlayerSprite.sprite = Players[CurrentPlayer].sprite;
        TxtName.text = Players[CurrentPlayer].Name;
        StartCoroutine(ImgAnim());
    }

    public void BackPlayer()
    {
        if (CurrentPlayer > 0)
            CurrentPlayer--;
        else
            CurrentPlayer = Players.Count -1;

        PlayerSprite.sprite = Players[CurrentPlayer].sprite;
        TxtName.text = Players[CurrentPlayer].Name;

        StartCoroutine(ImgAnim());
    }

    IEnumerator ImgAnim()
    {
        PlayerSprite.transform.position= new Vector2(PlayerSprite.transform.position.x + 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x - 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x - 4, PlayerSprite.transform.position.y);
        yield return new WaitForSeconds(0.05f);
        PlayerSprite.transform.position = new Vector2(PlayerSprite.transform.position.x + 4, PlayerSprite.transform.position.y);
    }
    public void PlayClick()
    {
        if (isMobile)
            TutorialMobile.SetActive(true);
        else
            TutorialPc.SetActive(true);

        PlayerPrefs.SetInt("IdPlayer", CurrentPlayer);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Scenary1");
    }
}

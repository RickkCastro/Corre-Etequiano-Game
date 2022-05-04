using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class MonetizationManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

    //Ads Extras
    public GameObject backfillInterstitial;
    public GameObject backfillRewarded;
    public Button closeButton;
    public Image closeImage;

    public bool AdsOff;

    private bool IsRewardedExtraAd;
    int timeout = 3;

    //Rewards
    string selectedRewardType = "";
    private bool giveUserReward = false;

    bool debugMode = false;

    //Android
    string gameID = "4471261";
    string interstitialID = "Interstitial_Android";
    string rewardedID = "Rewarded_Android";

    //Singleton
    private static MonetizationManager _Instance;
    public static MonetizationManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<MonetizationManager>();

                if (_Instance == null)
                {
                    GameObject monetizationObject = Instantiate(Resources.Load<GameObject>("DontDestroy/MonetizationManager"));
                    _Instance = monetizationObject.GetComponent<MonetizationManager>();
                }
            }

            return _Instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        debugMode = Debug.isDebugBuild;

        //Ads Extras
        closeButton.gameObject.SetActive(false);
        backfillInterstitial.SetActive(false);
        backfillRewarded.SetActive(false);

        Advertisement.debugMode = false;
        Advertisement.Initialize(gameID, debugMode, true, this);
    }

    public void OnInitializationComplete()
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnInitializationComplete");

        //Carregar anuncios
        Advertisement.Load(interstitialID, this);
        Advertisement.Load(rewardedID, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnInitializationFailed: " + error.ToString() + " | " + message);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsAdLoaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsFailedToLoad: " + placementId + " | " + error.ToString() + " | " + message);

        if(placementId == rewardedID)
            ShowExtraAd(15, true);
        else if(placementId == interstitialID)
            ShowExtraAd(3, false);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsShowFailure: " + placementId + " | " + error.ToString() + " | " + message);

        if(placementId == rewardedID)
            ShowExtraAd(15, true);
        else if(placementId == interstitialID)
            ShowExtraAd(3, false);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsShowStart: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsShowClick: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (debugMode) Debug.Log("[MonetizationManager] OnUnityAdsShowComplete: " + placementId + " | " + showCompletionState.ToString());

        //Rewards
        if (placementId.Trim().Equals(rewardedID) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            giveUserReward = true;
        }

        //Carregar mais um anuncio
        Advertisement.Load(placementId, this);
    }

    public void Update()
    {
        if (giveUserReward == true)
        {
            giveUserReward = false;
            RewardUser();
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void ShowInterstitial()
    {
        if (debugMode) Debug.Log("[MonetizationManager] ShowInterstitial");
        
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID, this);
        }
        
        //Ads extras
        else
        {
            ShowExtraAd(5, false);
        }
    }

    public void ShowRewarded(string rewardType)
    {
        if (debugMode) Debug.Log("[MonetizationManager] ShowRewarded: " + rewardType);
        selectedRewardType = rewardType;

        if (Advertisement.IsReady(rewardedID))
            Advertisement.Show(rewardedID, this);

        //Ads extras
        else
            ShowExtraAd(15, true);

        /* ShowExtraAd(15, true); */
    }

    private void ShowExtraAd(int Timeout, bool isRewarded)
    {
        timeout = Timeout;
        closeButton.gameObject.SetActive(true);
        backfillRewarded.SetActive(true);
        
        if(isRewarded)
            IsRewardedExtraAd = true;
    }

    private void RewardUser()
    {
        if (debugMode) Debug.Log("[MonetizationManager] RewardUser:" + selectedRewardType.ToString());
        
        switch (selectedRewardType)
        {
            case "Reborn":
                GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();
                gameScreens.Reborn();
                break;
            case "COINS_5": //Teste com moedas
                int coins = int.Parse(GameObject.Find("coins").GetComponent<TextMeshProUGUI>().text) + 5;
                GameObject.Find("coins").GetComponent<TextMeshProUGUI>().text = coins.ToString();
                break;
            default:
                break;
        }
    }

    public void CloseExtraAds()
    {
        closeButton.gameObject.SetActive(false);
        backfillInterstitial.SetActive(false);
        backfillRewarded.SetActive(false);

        if(IsRewardedExtraAd)
        {
            RewardUser();
            IsRewardedExtraAd = false;
        }
    }
}

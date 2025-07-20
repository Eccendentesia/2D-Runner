using UnityEngine;
using CrazyGames;

public class CrazyGamesAds : MonoBehaviour
{
    public GameObject Banner;
    public static CrazyGamesAds Instance;
    private AdModule ad;
    private BannerModule BM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // Initialize CrazyGames SDK
        CrazySDK.Init(() =>
        {
            Debug.Log("CrazyGames SDK Initialized");
            ad = CrazySDK.Ad;      // Initialize Ad Module
            BM = CrazySDK.Banner;  // Initialize Banner Module
            ShowBanner();
        });
      
    }
    private void Start()
    {
        ShowMidAd();
       
    }
    public void ShowMidAd()
    {
        if (ad == null)
        {
            Debug.LogWarning("AdModule not initialized yet.");
            return;
        }

        ad.RequestAd(
            CrazyAdType.Midgame,
            () => { Debug.Log("Ad Started"); },     // Ad start callback
            (error) => { Debug.LogError($"Ad Error: {error}"); }, // Ad error callback
            () => { Debug.Log("Ad Finished"); }    // Ad finish callback
        );
    }
    public void ShowRewardedAd()
    {
        if (ad == null)
        {
            Debug.LogWarning("AdModule not initialized yet.");
            return;
        }
        ad.RequestAd(
            CrazyAdType.Rewarded,
            () => { Debug.Log("Rewarded Ad Started"); }, // Ad start callback
            (error) => { Debug.LogError($"Rewarded Ad Error: {error}"); }, // Ad error callback
            () => {
                InGameUI.Instance.OnContinueButtonClicked(); // Call continue button click handler
                Debug.Log("Rewarded Ad Finished"); } // Ad finish callback
        );
    }

    public void ShowBanner()
    {
        if (BM == null)
        {
            Debug.LogWarning("BannerModule not initialized yet.");
            return;
        }

        BM.RefreshBanners();
        Banner.SetActive(true);
    }

    public void HideBanner()
    {
        if (BM == null)
        {
            Debug.LogWarning("BannerModule not initialized yet.");
            return;
        }

        Banner.SetActive(false);
        BM.RefreshBanners();
    }
    public void refresh()
    {
        BM.RefreshBanners();
    }
}

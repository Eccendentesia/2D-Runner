using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour
{
    public static MainMenuButtons Instance;

    [SerializeField] public GameObject MainMenu;
    [SerializeField] private GameObject UpgradesPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject LeaderBoardPanel;
    [SerializeField] private Button PlayButton;


   
    void Start()
    {
        MainMenu.SetActive(false);
        UpgradesPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        LeaderBoardPanel.SetActive(false);
        PlayButton.onClick.AddListener(loadGameScene);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(gameObject); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnUpgradesButtonClicked()
    {   
        CrazyGamesAds.Instance.refresh();    
        clickSound.Instance.playSound();
        UpgradesPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    
    public void OnSettingsButton()
    {
        CrazyGamesAds.Instance.refresh();
        clickSound.Instance.playSound();
        SettingsPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void OnLeaderBoardButtonClicked()
    {
        CrazyGamesAds.Instance.HideBanner();
        clickSound.Instance.playSound();
        LeaderBoardPanel.SetActive(true);
        MainMenu.SetActive(false);
    }
    //Deactivate Panels 
  
    public void OnUpgradesCancelButtonClicked()
    {
        clickSound.Instance.playSound();
        UpgradesPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
  
    public void OnSettingsCancelButton()
    {
        clickSound.Instance.playSound();
        SettingsPanel.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void OnLeaderBoardCancelButtonClicked()
    {
        CrazyGamesAds.Instance.ShowBanner();
        clickSound.Instance.playSound();
        LeaderBoardPanel.SetActive(false);
        MainMenu.SetActive(true);
       
    }
    private void loadGameScene()
    {
        CrazyGamesAds.Instance.HideBanner(); 
        clickSound.Instance.playSound();
        MainMenu.SetActive(false);
        LoadingScene.Instance.LoadSceneByName("GameScene"); 

    }

}

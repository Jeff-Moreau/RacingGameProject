using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static MenuManager myInstance;
    private void Singleton()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            myInstance = this;
        }
    }
    public static MenuManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [SerializeField] private GameObject theMainMenu = null;
    [SerializeField] private GameObject theGameManager = null;
    [SerializeField] private GameObject theLoadingManager = null;
    [SerializeField] private GameObject theRaceManager = null;
    [SerializeField] private GameObject thePlayingHUD = null;
    [SerializeField] private GameObject theCenterUpdates = null;
    [SerializeField] private GameObject theIntro = null;
    [SerializeField] private GameObject theVideo = null;
    [SerializeField] private GameObject theNewGameMenu = null;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        Invoke(nameof(ShowIntro), 1.7f);
    }

    public void ShowIntro()
    {
        theVideo.SetActive(true);
        Invoke(nameof(GoToMainMenu), 3.6f);
    }

    public void GoToMainMenu()
    {
        theIntro.SetActive(false);
        theMainMenu.SetActive(true);
    }

    public void NewGame()
    {
        theMainMenu.SetActive(false);
        theNewGameMenu.SetActive(true);
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void JoinGame()
    {

    }

    public void HostGame()
    {

    }

    public void BackToMain()
    {
        theMainMenu.SetActive(true);
    }

    public void NextRace()
    {

    }
}

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


    private void Awake()
    {
        Singleton();
    }

    public void NewGame()
    {
        theMainMenu.SetActive(false);
        theGameManager.SetActive(true);
        theLoadingManager.SetActive(true);
        theRaceManager.SetActive(true);
        thePlayingHUD.SetActive(true);
        theCenterUpdates.SetActive(true);
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

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

    [SerializeField] private GameObject theMainMenu;

    public void NewGame()
    {
        theMainMenu.SetActive(false);
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

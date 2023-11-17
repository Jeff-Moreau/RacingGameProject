using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _aiVehicles;
    [SerializeField] private GameObject _playerVehicle;
    [SerializeField] private GameObject _managers;

    public void NewGame()
    {
        _mainMenu.SetActive(false);
        _managers.SetActive(true);
        _aiVehicles.SetActive(true);
        _playerVehicle.SetActive(true);
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
}

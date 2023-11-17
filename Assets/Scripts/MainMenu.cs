using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _aiVehicles;
    [SerializeField] private GameObject _playerVehicle;
    [SerializeField] private GameObject _managers;
    [SerializeField] private RaceManager _raceManager;
    [SerializeField] private GameObject _finish;  

    public void NewGame()
    {
        _mainMenu.SetActive(false);
        _managers.SetActive(true);
        _aiVehicles.SetActive(true);
        _playerVehicle.SetActive(true);
        _raceManager.ResetLevel();
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
        _managers.SetActive(false);
        _aiVehicles.SetActive(false);
        _playerVehicle.SetActive(false);
        _finish.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void NextRace()
    {

    }
}

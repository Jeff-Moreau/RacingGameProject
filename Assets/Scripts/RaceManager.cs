using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // spawn everyone
    // wait for count down
    // start race
    // check laps
    // update lap text
    // if position changes flash on screen for 1 - 3
    [SerializeField] private GameObject _countDownWindow;
    [SerializeField] private TextMeshProUGUI _centerText;
    [SerializeField] private TextMeshProUGUI _lapText;
    [SerializeField] private TextMeshProUGUI _positionText;
    [SerializeField] private Collider _finishLine;

    private bool _gameStart = false;
    private float _secondsPassed = 4;
    private int _trackLaps = 5;
    private int _currentLap = 0;

    public bool GameStarted => _gameStart;
    public int CurrentLap => _currentLap;
    public int SetCurrentLap(int amount) => _currentLap += amount;

    private void Update()
    {
        _secondsPassed -= Time.deltaTime;

        if (!_gameStart && _secondsPassed > 1)
        {
            _centerText.text = Mathf.Floor(_secondsPassed).ToString();
        }
        else if (_secondsPassed < 1 && _secondsPassed >= -1)
        {
            _centerText.text = "GO";
            _gameStart = true;
        }
        else
        {
            _centerText.text = "";
        }

        if (_gameStart)
        {
            _lapText.text = "Lap: " + _currentLap + " of " + _trackLaps;
        }
    }
}

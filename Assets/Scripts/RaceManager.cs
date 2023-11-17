using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI _updateText;
    [SerializeField] private TextMeshProUGUI _lapText;
    [SerializeField] private TextMeshProUGUI _positionText;
    [SerializeField] private Collider _finishLine;
    [SerializeField] private VehicleMovement _player;
    [SerializeField] private LoadingManager _loadingManager;
    [SerializeField] private AudioClip[] _countdown;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<GameObject> _racers;

    private bool _gameStart = false;
    private bool _raceOver = true;
    private bool _resetText = false;
    private float _secondsPassed = 4;
    private int _trackLaps = 2;
    private int _currentLap = 0;
    private int _count = 0;
    private int _currentPosition = 0;
    

    public bool GameStarted => _gameStart;
    public int CurrentLap => _currentLap;
    public int SetCurrentLap(int amount) => _currentLap += amount;
    public int CurrentPosition => _currentPosition;
    public int SetCurrentPosition(int amount) => _currentPosition = amount;
    public void AddRacers(GameObject me) => _racers.Add(me);
    public void ResetRacers() => _racers.Clear();
    public int GetRacers => _racers.Count;

    private void Update()
    {
        _secondsPassed -= Time.deltaTime;
        Debug.Log(_racers.Count);

        if (!_gameStart && _secondsPassed > 1)
        {

            if (Mathf.Floor(_secondsPassed) == 3)
            {
                if (!_audio.isPlaying && _count == 0)
                {
                    _audio.PlayOneShot(_countdown[0]);
                    _count++;
                }
            }
            else if (Mathf.Floor(_secondsPassed) == 2)
            {
                if (!_audio.isPlaying && _count == 1)
                {
                    _audio.PlayOneShot(_countdown[1]);
                    _count++;
                }
            }
            else if (Mathf.Floor(_secondsPassed) == 1)
            {
                if (!_audio.isPlaying && _count == 2)
                {
                    _audio.PlayOneShot(_countdown[2]);
                    _count++;
                }
            }
            _centerText.text = Mathf.Floor(_secondsPassed).ToString();
        }
        else if (_secondsPassed < 1 && _secondsPassed >= -1)
        {
            if (!_audio.isPlaying && _count == 3)
            {
                _audio.PlayOneShot(_countdown[3]);
                _count = 0;
            }
            _centerText.text = "GO";
            _gameStart = true;
        }
        else
        {
            _centerText.text = "";
        }

        if (_gameStart && _currentLap <= _trackLaps)
        {
            _lapText.text = "Lap: " + _currentLap + " of " + _trackLaps;
            _positionText.text = "Position: " + _player.CurrentPosition + " of " + (_loadingManager.RaceCount);
        }
        if (_currentLap == _trackLaps && !_resetText)
        {
            if (!_audio.isPlaying && _count == 0)
            {
                _audio.PlayOneShot(_countdown[4]);
                _count++;
            }
            _updateText.text = "LAST LAP!";
            Invoke("ResetLapText", 2);
        }
        if (_currentLap > _trackLaps)
        {
            _lapText.text = "Lap: " + _trackLaps + " of " + _trackLaps;
            _updateText.text = "  YOU FINISHED  " + _player.CurrentPosition;
            _raceOver = true;
        }
    }

    private void ResetLapText()
    {
        _resetText = true;
        _updateText.text = "";
    }
}

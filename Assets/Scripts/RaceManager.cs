using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static RaceManager myInstance;
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

        DontDestroyOnLoad(this.gameObject);
    }
    public static RaceManager Load => myInstance;
    // SINGLETON ENDS

    [SerializeField] private GameObject _countDownWindow;
    [SerializeField] private TextMeshProUGUI _centerText;
    [SerializeField] private TextMeshProUGUI _updateText;
    [SerializeField] private TextMeshProUGUI _lapText;
    [SerializeField] private TextMeshProUGUI _positionText;
    [SerializeField] private Collider _finishLine;
    [SerializeField] private PlayerController _player;
    [SerializeField] private LoadingManager _loadingManager;
    [SerializeField] private AudioClip[] _countdown;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<GameObject> _racers;
    [SerializeField] private GameObject _finish;
    [SerializeField] private LoadingManager _loadManager;

    private bool _gameStart = false;
    private bool _raceOver = false;
    private bool _resetText = false;
    private float _secondsPassed = 4;
    private int _trackLaps = 3;
    private int _currentLap = 0;
    private int _count = 0;

    public bool RaceOver => _raceOver;
    public bool GameStarted => _gameStart;
    public int SetCurrentLap(int amount) => _currentLap += amount;
    public void AddRacers(GameObject me) => _racers.Add(me);
    public void ResetRacers() => _racers.Clear();
    public int GetRacers => _racers.Count;

    private void Awake()
    {
        Singleton();
        _gameStart = false;
        _raceOver = false;
        _resetText = false;
        _secondsPassed = 4;
        _trackLaps = 3;
        _currentLap = 0;
        _count = 0;
    }

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
            _positionText.text = "Position: " + _player.CurrentPosition + " of " + (_loadingManager.RaceCount);
            _lapText.text = "Lap: " + _trackLaps + " of " + _trackLaps;
            _updateText.text = "  YOU FINISHED  " + _player.CurrentPosition;
            _raceOver = true;
            _finish.SetActive(true);
        }
    }

    private void ResetLapText()
    {
        _resetText = true;
        _updateText.text = "";
    }

    public void ResetLevel()
    {
        _gameStart = false;
        _raceOver = false;
        _resetText = false;
        _secondsPassed = 4;
        _trackLaps = 3;
        _currentLap = 0;
        _count = 0;
        //_loadManager.ResetLoading();
        _racers.Clear();
        _positionText.text = "";
        _lapText.text = "";
        _updateText.text = "";
    }
}

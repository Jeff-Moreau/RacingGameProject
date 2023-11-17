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

    private bool _gameStart = false;
    private float _secondsPassed = 4;

    public bool GameStarted => _gameStart;

    private void Update()
    {
        _secondsPassed -= Time.deltaTime;

        if (!_gameStart && _secondsPassed > 1)
        {
            _centerText.text = Mathf.Floor(_secondsPassed).ToString();
        }
        else
        {
            _centerText.text = "GO";
            _gameStart = true;
        }
    }
}

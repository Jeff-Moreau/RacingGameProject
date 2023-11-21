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
    }
    public static RaceManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [Header("Audio")]
    [NonReorderable]
    [SerializeField] private AudioClip[] myCountDownAudioClips;
    [SerializeField] private AudioSource myAudioSource;

    [Header("Other Data Needed")]
    [SerializeField] private TextMeshProUGUI theCountDownText;
    [SerializeField] private TextMeshProUGUI theUpdateText;
    [SerializeField] private TextMeshProUGUI theLapText;
    [SerializeField] private TextMeshProUGUI thePositionText;
    [SerializeField] private GameObject theFinishUI;

    // LOCAL VARIABLES
    private List<GameObject> myRacers;
    private bool myGameStart;
    private bool myRaceOver;
    private bool myResetText;
    private float myStartRaceCountdownSeconds;
    private int myTrackLaps; // to the track itself
    private int myCurrentLap;
    private int myAudioCount;
    private int thePlayerPosition;
    private GameObject thePlayer;

    public bool RaceOver => myRaceOver;
    public bool GameStarted => myGameStart;
    public void AddRacers(GameObject me) => myRacers.Add(me);
    public void ResetRacers() => myRacers.Clear();
    public int GetRacers => myRacers.Count;
    public int SetPlayerPosition(int position) => thePlayerPosition = position;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        myGameStart = false;
        myRaceOver = false;
        myResetText = false;
        myStartRaceCountdownSeconds = 4;
        myTrackLaps = LoadingManager.Load.GetCurrentTrackLaps;
        Debug.Log("Race " + LoadingManager.Load.GetCurrentTrackLaps);
        myCurrentLap = 0;
        myAudioCount = 0;
        myRacers = LoadingManager.Load.GetTotalVehicles;
        thePlayer = LoadingManager.Load.GetPlayer;
    }

    private void Update()
    {
        myStartRaceCountdownSeconds -= Time.deltaTime;

        if (!myGameStart && myStartRaceCountdownSeconds > 1)
        {
            if (Mathf.Floor(myStartRaceCountdownSeconds) == 3)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 0)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[0]);
                    myAudioCount++;
                }
            }
            else if (Mathf.Floor(myStartRaceCountdownSeconds) == 2)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 1)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[1]);
                    myAudioCount++;
                }
            }
            else if (Mathf.Floor(myStartRaceCountdownSeconds) == 1)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 2)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[2]);
                    myAudioCount++;
                }
            }
            theCountDownText.text = Mathf.Floor(myStartRaceCountdownSeconds).ToString();
        }
        else if (myStartRaceCountdownSeconds < 1 && myStartRaceCountdownSeconds >= -1)
        {
            if (!myAudioSource.isPlaying && myAudioCount == 3)
            {
                myAudioSource.PlayOneShot(myCountDownAudioClips[3]);
                myAudioCount = 0;
            }
            theCountDownText.text = "GO";
            myGameStart = true;
        }
        else
        {
            theCountDownText.text = "";
        }

        if (myGameStart && myCurrentLap <= myTrackLaps)
        {
            theLapText.text = "Lap: " + myCurrentLap + " of " + myTrackLaps;
            thePositionText.text = "Position: " + thePlayerPosition + " of " + (LoadingManager.Load.GetTrackPolePositions);
        }
        if (myCurrentLap == myTrackLaps && !myResetText)
        {
            if (!myAudioSource.isPlaying && myAudioCount == 0)
            {
                myAudioSource.PlayOneShot(myCountDownAudioClips[4]);
                myAudioCount++;
            }
            theUpdateText.text = "LAST LAP!";
            Invoke(nameof(ResetLapText), 2);
        }
        if (myCurrentLap > myTrackLaps)
        {
            thePositionText.text = "Position: " + thePlayerPosition + " of " + (LoadingManager.Load.GetTrackPolePositions);
            theLapText.text = "Lap: " + myTrackLaps + " of " + myTrackLaps;
            theUpdateText.text = "  YOU FINISHED  " + thePlayerPosition;
            myRaceOver = true;
            theFinishUI.SetActive(true);
        }
    }

    private void ResetLapText()
    {
        myResetText = true;
        theUpdateText.text = "";
    }

    public void ResetLevel()
    {
        myGameStart = false;
        myRaceOver = false;
        myResetText = false;
        myStartRaceCountdownSeconds = 4;
        myTrackLaps = 3;
        myCurrentLap = 0;
        myAudioCount = 0;
        //myRacers.Clear();
        thePositionText.text = "";
        theLapText.text = "";
        theUpdateText.text = "";
    }
}

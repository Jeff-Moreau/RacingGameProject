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

    [Header("Race Data")]
    [SerializeField] private AudioClip[] myCountDownAudioClips;
    [SerializeField] private AudioSource myAudioSource;

    [Header("Other Data Needed")]
    [SerializeField] private GameObject[] theTracks;
    [SerializeField] private TextMeshProUGUI theCountDownText;
    [SerializeField] private TextMeshProUGUI theUpdateText;
    [SerializeField] private TextMeshProUGUI theLapText;
    [SerializeField] private TextMeshProUGUI thePositionText;
    [SerializeField] private GameObject theFinishUI;

    //private List<GameObject> myRacers = null;
    private bool myGameStart = false;
    private bool myRaceOver = false;
    private bool myResetText = false;
    private float myStartRaceCountdownSeconds = 4;
    private int myTrackLaps = 3; // to the track itself
    private int myCurrentLap = 0;
    private int myAudioCount = 0;

    public bool RaceOver => myRaceOver;
    public bool GameStarted => myGameStart;
    //public void AddRacers(GameObject me) => myRacers.Add(me);
    //public void ResetRacers() => myRacers.Clear();
    //public int GetRacers => myRacers.Count;

    private void Awake()
    {
        Singleton();
        myGameStart = false;
        myRaceOver = false;
        myResetText = false;
        myStartRaceCountdownSeconds = 4;
        myTrackLaps = 3;
        myCurrentLap = 0;
        myAudioCount = 0;
    }

    private void Update()
    {
        myStartRaceCountdownSeconds -= Time.deltaTime;
        //Debug.Log(myRacers.Count);

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
            thePositionText.text = "Position: " + /*LoadingManager.Load.GetPlayers[0].GetComponent<VehicleController>().CurrentPosition +*/ " of " + (LoadingManager.Load.RaceCount);
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
            thePositionText.text = "Position: "/* + LoadingManager.Load.GetPlayers[0].GetComponent<VehicleController>().CurrentPosition*/ + " of " + (LoadingManager.Load.RaceCount);
            theLapText.text = "Lap: " + myTrackLaps + " of " + myTrackLaps;
            theUpdateText.text = "  YOU FINISHED  "/* + LoadingManager.Load.GetPlayers[0].GetComponent<VehicleController>().CurrentPosition*/;
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

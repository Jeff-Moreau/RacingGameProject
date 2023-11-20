using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static LoadingManager myInstance;
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
    public static LoadingManager Load => myInstance;
    // SINGLETON ENDS

    [Header("Other Data Needed")]
    [SerializeField] private GameObject[] theTrack; // fix later with list of tracks
    [SerializeField] private AIVehiclePool theAIVehiclePool;
    [SerializeField] private GameObject[] thePlayerVehicles;
    
    private List<GameObject> theTotalVehicles;
    private int theTotalAIVehicles;
    private int theTotalPlayers;

    private TrackInformation theCurrentTrack;
    private GameObject[] theTrackPolePositions;
    private PolePositionMarker[] theTrackPolePositionMarkers;

    public int RaceCount => theTrackPolePositions.Length;
    public GameObject GetCurrentTrack => theTrack[0];
    public GameObject GetPlayer => thePlayerVehicles[0];
    public List<GameObject> GetTotalVehicles => theTotalVehicles;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        theCurrentTrack = theTrack[0].GetComponent<TrackInformation>();
        Instantiate(theTrack[0]);
        theTrackPolePositions = theCurrentTrack.GetPolePositions;
        theTrackPolePositionMarkers = theCurrentTrack.GetPolePositionMarkers;
        theAIVehiclePool.SetTotalAIVehcilesNeeded(theTrackPolePositions.Length - thePlayerVehicles.Length);
        theTotalVehicles = new List<GameObject>();
        theTotalAIVehicles = 0;
        theTotalPlayers = 0;
    }

    private void Update()
    {
        ResetPolePositions();
        LoadPlayersInTrack();
        LoadAIVehiclesInTrack();
    }

    private void ResetPolePositions()
    {
        for (int i = 0; i < theTrackPolePositionMarkers.Length; i++)
        {
            theTrackPolePositionMarkers[i].SetSpotTaken(false);
        }
    }

    private void LoadPlayersInTrack()
    {
        for (int i = 0; i < theTrackPolePositions.Length; i++)
        {
            for (int j = 0; j < thePlayerVehicles.Length; j++)
            {
                var randomPolePosition = Random.Range(0, thePlayerVehicles.Length);
                Debug.Log("random " + randomPolePosition + "Array "+ theTrackPolePositionMarkers.Length);
                if (theTrackPolePositionMarkers[randomPolePosition].GetSpotTaken == false && theTotalPlayers < thePlayerVehicles.Length)
                {
                    thePlayerVehicles[j].transform.SetPositionAndRotation(theTrackPolePositions[randomPolePosition].transform.position, theTrackPolePositions[randomPolePosition].transform.rotation);
                    theTrackPolePositionMarkers[randomPolePosition].SetSpotTaken(true);
                    Instantiate(thePlayerVehicles[j]);
                    thePlayerVehicles[j].SetActive(true);
                    theTotalVehicles.Add(thePlayerVehicles[j]);
                    theTotalPlayers++;
                    theTotalAIVehicles++;
                }
            }
        }
    }

    private void LoadAIVehiclesInTrack()
    {
        for (int i = 0; i < theTrackPolePositions.Length; i++)
        {
            if (theTrackPolePositionMarkers[i].GetSpotTaken == false && theTotalAIVehicles < theTrackPolePositions.Length)
            {
                var newAIVehicle = theAIVehiclePool.GetAIVehicle();

                if (newAIVehicle != null)
                {
                    newAIVehicle.transform.SetPositionAndRotation(theTrackPolePositions[i].transform.position, theTrackPolePositions[i].transform.rotation);
                    theTrackPolePositionMarkers[i].SetSpotTaken(true);
                    theTotalVehicles.Add(newAIVehicle);
                    theTotalAIVehicles++;
                }

                for (int j = 0; j < theAIVehiclePool.GetAIVehicleList.Count; j++)
                {
                    newAIVehicle.SetActive(true);
                }
            }
        }
    }
}
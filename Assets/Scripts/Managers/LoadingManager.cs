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

    [Header("Loading Data")]
    [SerializeField, Range(1, 20)] private int myVehicleCount; // change based on track and players not a solid number

    [Header("Other Data Needed")]
    [SerializeField] private AIVehiclePool theAIVehiclePool;
    [SerializeField] private GameObject[] thePlayerVehicles;
    [SerializeField] private GameObject[] theTrack; // fix later with list of tracks
    
    private GameObject[] thePolePositions;
    private List<GameObject> theTotalVehicles;
    private int theTotalAIVehicles;
    private int theTotalPlayers;

    public int RaceCount => myVehicleCount;
    public GameObject GetCurrentTrack => theTrack[0];
    public GameObject GetPlayer => thePlayerVehicles[0];
    public List<GameObject> GetTotalVehicles => theTotalVehicles;

    private void Awake()
    {
        Singleton();
        thePolePositions = theTrack[0].GetComponent<TrackInformation>().GetPolePositions;
        //theAIVehiclePool.SetTotalAIVehciles(thePolePositions.Length - thePlayerVehicles.Length);
    }

    private void Start()
    {
        theTotalVehicles = new List<GameObject>();
        Instantiate(theTrack[0]);
    }

    private void Update()
    {
        ResetPolePositions();
        LoadPlayersInTrack();
        LoadAIVehiclesInTrack();
        
    }

    private void ResetPolePositions()
    {
        for (int i = 0; i < thePolePositions.Length; i++)
        {
            thePolePositions[i].GetComponent<PolePositionMarker>().SetSpotTaken(false);
        }
    }

    private void LoadPlayersInTrack()
    {
        for (int i = 0; i < thePolePositions.Length; i++)
        {
            for (int j = 0; j < thePlayerVehicles.Length; j++)
            {
                var randomPolePosition = Random.Range(0, thePlayerVehicles.Length);

                if (thePolePositions[randomPolePosition].GetComponent<PolePositionMarker>().GetSpotTaken == false && theTotalPlayers < thePlayerVehicles.Length)
                {
                    thePlayerVehicles[j].transform.SetPositionAndRotation(thePolePositions[randomPolePosition].transform.position, thePolePositions[randomPolePosition].transform.rotation);
                    thePolePositions[randomPolePosition].GetComponent<PolePositionMarker>().SetSpotTaken(true);
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
        for (int i = 0; i < thePolePositions.Length; i++)
        {
            if (thePolePositions[i].GetComponent<PolePositionMarker>().GetSpotTaken == false && theTotalAIVehicles < myVehicleCount)
            {
                var newAIVehicle = theAIVehiclePool.GetAIVehicle();

                if (newAIVehicle != null)
                {
                    newAIVehicle.transform.SetPositionAndRotation(thePolePositions[i].transform.position, thePolePositions[i].transform.rotation);
                    thePolePositions[i].GetComponent<PolePositionMarker>().SetSpotTaken(true);
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
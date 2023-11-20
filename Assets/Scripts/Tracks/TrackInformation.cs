using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    [SerializeField] private TrackData myData;
    [SerializeField] private Transform[] myWaypoints;
    [SerializeField] private GameObject[] myPolePositions;
    [SerializeField] private GameObject myWaypointsContainer;

    private string myTrackName;
    private int myTrackLaps;
    private PolePositionMarker[] myPolePositionMarkers;

    public Transform[] GetWaypoints => myWaypoints;
    public GameObject[] GetPolePositions => myPolePositions;
    public GameObject GetWaypointContainer => myWaypointsContainer;
    public string GetName => myTrackName;
    public int GetLaps => myTrackLaps;
    public PolePositionMarker[] GetPolePositionMarkers => myPolePositionMarkers;

    private void Awake()
    {
        myPolePositionMarkers = new PolePositionMarker[myPolePositions.Length];

        for (int i = 0; i < myPolePositions.Length; i++)
        {
            myPolePositionMarkers[i] = myPolePositions[i].GetComponent<PolePositionMarker>();
        }
        
    }

    private void Start()
    {
        Debug.Log("Markers Length " + myPolePositionMarkers.Length);

        myTrackName = myData.GetName;
        myTrackLaps = myData.GetLaps;
    }

}

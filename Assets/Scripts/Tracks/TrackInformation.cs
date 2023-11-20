using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    [SerializeField] private TrackData myData;
    [SerializeField] private Transform[] myWaypoints;
    [SerializeField] private GameObject[] myPolePositions;
    [SerializeField] private GameObject myWaypointsContainer;

    private string myTrackName;
    private int myTrackLaps;

    public Transform[] GetTrackWaypoints => myWaypoints;
    public GameObject[] GetTrackPolePositions => myPolePositions;
    public GameObject GetTrackWaypointContainer => myWaypointsContainer;
    public string GetTrackName => myTrackName;
    public int GetTrackLaps => myTrackLaps;

    private void Start()
    {
        myTrackName = myData.GetName;
        myTrackLaps = myData.GetLaps;
    }

}

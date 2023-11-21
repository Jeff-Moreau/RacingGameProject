using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    [SerializeField] private TrackData myData;
    [SerializeField] private Transform[] myWaypoints;
    [SerializeField] private GameObject[] myPolePositions;
    [SerializeField] private GameObject myWaypointsContainer;

    private string myTrackName;
    private int myTrackLaps;

    public Transform[] GetWaypoints => myWaypoints;
    public GameObject[] GetPolePositions => myPolePositions;
    public GameObject GetWaypointContainer => myWaypointsContainer;
    public string GetName => myTrackName;
    public int GetLaps => myTrackLaps;

    private void Start()
    {
        myTrackName = myData.GetName;
        myTrackLaps = myData.GetLaps;
    }
}

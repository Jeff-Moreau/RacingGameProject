using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    [SerializeField] private TrackData myData;

    [NonReorderable]
    [SerializeField] private Transform[] myWaypoints;

    [NonReorderable]
    [SerializeField] private GameObject[] myPolePositions;

    [SerializeField] private GameObject myWaypointsContainer;

    public Transform[] GetWaypoints => myWaypoints;
    public GameObject[] GetPolePositions => myPolePositions;
    public GameObject GetWaypointContainer => myWaypointsContainer;
    public string GetName => myData.GetName;
    public int GetLaps => myData.GetLaps;
}

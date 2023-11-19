using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    [SerializeField] private GameObject myWaypointsContainer;
    [SerializeField] private Transform[] myWaypoints;
    [SerializeField] private GameObject[] myPolePositions;

    public GameObject[] GetPolePositions => myPolePositions;
    public GameObject GetWaypointsContainer => myWaypointsContainer;
    public Transform[] GetMyWaypoints => myWaypoints;
}

using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Vehicle Pieces")]
    [SerializeField] protected Rigidbody mySphere = null;
    [SerializeField] protected GameObject myArmor = null;
    [SerializeField] protected ParticleSystem[] myExhaustParticles = null;
    [SerializeField] protected ParticleSystem[] myThrusterParticles = null;
    [SerializeField] protected Light[] myTailLightBulbs = null;
    [SerializeField] protected Light[] myHeadLightBulbs = null;

    protected GameObject theTrackWaypointContainer;
    protected Transform[] myTrackWaypointsToFollow;
    protected int myCurrentRacePosition;
    protected int myCurrentTrackWaypoint;
    protected float myProximityToCurrentWaypoint;

    protected void Awake()
    {
        theTrackWaypointContainer = LoadingManager.Load.GetCurrentTrack.GetComponent<TrackInformation>().GetWaypointContainer;
    }

    protected void GetWaypoints()
    {
        var potentialWaypoints = theTrackWaypointContainer.GetComponentsInChildren<Transform>();
        myTrackWaypointsToFollow = new Transform[potentialWaypoints.Length - 1];

        for (int i = 1; i < potentialWaypoints.Length; i++)
        {
            myTrackWaypointsToFollow[i - 1] = potentialWaypoints[i];
        }
    }

    public Transform GetLastWaypoint()
    {
        if (myCurrentTrackWaypoint - 1 < 0)
        {
            return myTrackWaypointsToFollow[^ - 1];
        }
        else
        {
            return myTrackWaypointsToFollow[myCurrentTrackWaypoint - 1];
        }
    }

    protected void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        if (relativeWaypointPos.sqrMagnitude < myProximityToCurrentWaypoint)
        {
            myCurrentTrackWaypoint += 1;

            if (myCurrentTrackWaypoint == myTrackWaypointsToFollow.Length)
            {
                myCurrentTrackWaypoint = 0;
            }
        }
    }
}
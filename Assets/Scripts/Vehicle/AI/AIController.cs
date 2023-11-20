using UnityEngine;

public class AIController : VehicleController
{
    [Header("Vehicle Data")]
    [SerializeField] protected VehicleData myData;

    private void Start()
    {
        GetWaypoints();
        myCurrentTrackWaypoint = 14;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;
    }

    private void Update()
    {
        var waypointPosition = myTrackWaypointsToFollow[myCurrentTrackWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        if (RaceManager.Load.GameStarted)
        {
            mySphere.AddForce(myArmor.transform.forward * myData.GetRollSpeed, ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor.transform.rotation = myTrackWaypointsToFollow[myCurrentTrackWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }
}
using UnityEngine;

public class AIController : VehicleController
{
    [Header("Vehicle Data")]
    [SerializeField] protected VehicleData myData;

    private float myWaypointProximity;

    private void Start()
    {
        GetWaypoints();
        myCurrentWaypoint = 14;
        myWaypointProximity = myData.GetWaypointProximity * myData.GetWaypointProximity;
    }

    private void Update()
    {
        var waypointPosition =myWaypoints[myCurrentWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        if (RaceManager.Load.GameStarted)
        {
            mySphere.AddForce(myArmor.transform.forward * myData.GetRollSpeed, ForceMode.Force); // change after set to 50
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor.transform.rotation = myWaypoints[myCurrentWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }

    private void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        if (relativeWaypointPos.sqrMagnitude < myWaypointProximity)
        {
            myCurrentWaypoint += 1;

            if (myCurrentWaypoint == myWaypoints.Length)
            {
                myCurrentWaypoint = 0;
            }
        }
    }
}
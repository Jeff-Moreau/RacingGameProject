using UnityEngine;

public class AIController : VehicleController
{
    // INSPECTOR VARIABLES
    [Header("Scriptable Object Data")]
    [SerializeField] protected VehicleData myData = null;

    private void Start()
    {
        InitializeVariables();
        GetWaypoints();
    }

    private void Update()
    {
        AutoDrive();
    }

    private void InitializeVariables()
    {
        myCurrentTrackWaypoint = 14;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;

        for (int i = 0; i < myThrusterParticles.Length; i++)
        {
            myThrusterParticles[i].gameObject.SetActive(false);
        }
    }

    private void AutoDrive()
    {
        var waypointPosition = theTrackWaypointsToFollow[myCurrentTrackWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        if (RaceManager.Load.GetGameStarted)
        {
            for (int i = 0; i < myThrusterParticles.Length; i++)
            {
                myThrusterParticles[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < myExhaustParticles.Length; i++)
            {
                myExhaustParticles[i].gameObject.SetActive(false);
            }

            //mySphere.AddForce(myArmor.transform.forward * myData.GetRollSpeed, ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            //myArmor.transform.rotation = theTrackWaypointsToFollow[myCurrentTrackWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }
}
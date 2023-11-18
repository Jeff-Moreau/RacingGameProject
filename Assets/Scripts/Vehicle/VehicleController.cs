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

    protected GameObject theWaypointsContainer;
    protected Transform[] myWaypoints;
    protected int myCurrentPosition;
    protected int myCurrentWaypoint;

    public int CurrentPosition => myCurrentPosition;

    protected void Awake()
    {
        theWaypointsContainer = GameObject.FindWithTag("Waypoints");
    }

    protected void GetWaypoints()
    {
        var potentialWaypoints = theWaypointsContainer.GetComponentsInChildren<Transform>();
        myWaypoints = new Transform[potentialWaypoints.Length - 1];

        for (int i = 1; i < potentialWaypoints.Length; i++)
        {
            myWaypoints[i - 1] = potentialWaypoints[i];
        }
    }

    public Transform GetLastWaypoint()
    {
        if (myCurrentWaypoint - 1 < 0)
        {
            return myWaypoints[myWaypoints.Length - 1];
        }
        else
        {
            return myWaypoints[myCurrentWaypoint - 1];
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;
    }
}
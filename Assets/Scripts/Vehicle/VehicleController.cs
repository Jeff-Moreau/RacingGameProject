using Palmmedia.ReportGenerator.Core;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [Header("For Saving and Loading")]
    [SerializeField] protected string myVehicleName = null;
    [SerializeField] protected float myMaterialRed = 0;
    [SerializeField] protected float myMaterialGreen = 0;
    [SerializeField] protected float myMaterialBlue = 0;
    [SerializeField] protected float myMaterialAlpha = 1;
    [SerializeField] protected Material myBallMaterial = null;
    [SerializeField] protected int myArmorType = 0;

    [Header("Vehicle Pieces")]
    [SerializeField] protected Rigidbody mySphere = null;
    [SerializeField] protected Renderer myRenderer = null;
    [SerializeField] protected GameObject[] myArmor = null;
    [SerializeField] protected ParticleSystem[] myExhaustParticles = null;
    [SerializeField] protected ParticleSystem[] myThrusterParticles = null;
    [SerializeField] protected Light[] myTailLightBulbs = null;
    [SerializeField] protected Light[] myHeadLightBulbs = null;

    // LOCAL VARIABLES
    protected GameObject theTrackWaypointContainer;
    protected Transform[] theTrackWaypointsToFollow;
    protected int myCurrentRacePosition;
    protected int myCurrentTrackWaypoint;
    protected float myProximityToCurrentWaypoint;

    // GETTERS
    public string GetName => myVehicleName;

    // SETTERS
    public int SetArmorType(int num) => myArmorType = num;
    public string SetName(string vehicle) => myVehicleName = vehicle;
    public float SetMaterialRed(float num) => myMaterialRed = num;
    public float SetMaterialGreen(float num) => myMaterialGreen = num;
    public float SetMaterialBlue(float num) => myMaterialBlue = num;
    public float SetMaterialAlpha(float num) => myMaterialAlpha = num;
    public Material SetBallMaterial(Material mat) => myBallMaterial = mat;

    protected void Awake()
    {
        theTrackWaypointContainer = LoadingManager.Load.GetCurrentTrackInformation.GetWaypointContainer;
    }

    protected void GetWaypoints()
    {
        var potentialWaypoints = theTrackWaypointContainer.GetComponentsInChildren<Transform>();
        theTrackWaypointsToFollow = new Transform[potentialWaypoints.Length - 1];

        for (int i = 1; i < potentialWaypoints.Length; i++)
        {
            theTrackWaypointsToFollow[i - 1] = potentialWaypoints[i];
        }
    }

    protected Transform GetLastWaypoint()
    {
        if (myCurrentTrackWaypoint - 1 < 0)
        {
            return theTrackWaypointsToFollow[^ - 1];
        }
        else
        {
            return theTrackWaypointsToFollow[myCurrentTrackWaypoint - 1];
        }
    }

    protected void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        if (relativeWaypointPos.sqrMagnitude < myProximityToCurrentWaypoint)
        {
            myCurrentTrackWaypoint += 1;

            if (myCurrentTrackWaypoint == theTrackWaypointsToFollow.Length)
            {
                myCurrentTrackWaypoint = 0;
            }
        }
    }
}
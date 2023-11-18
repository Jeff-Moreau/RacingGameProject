using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private VehicleData myData;
    [SerializeField] private Rigidbody myBody;
    [SerializeField] private GameObject myArmor;
    [SerializeField] private ParticleSystem myExhaustParticle;
    [SerializeField] private ParticleSystem myThrusterParticle;
    [SerializeField] private Light[] myTailLights;

    [SerializeField] private GameObject theWaypointContainer;
    [SerializeField] private RaceManager theRaceManager;
    [SerializeField] private LoadingManager theLoadManager;

    private Transform[] _waypoints;
    private int _currentWaypoint;
    private float _proxSqr;

    public Transform GetCurrentWaypoint => _waypoints[_currentWaypoint];

    private void Start()
    {
        GetWaypoints();
        _currentWaypoint = 14;
        _proxSqr = 8 * 8; // change after
    }

    private void Update()
    {
        var waypointPosition = _waypoints[_currentWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        if (theRaceManager.GameStarted)
        {
            myBody.AddForce(myArmor.transform.forward * 50, ForceMode.Force); // change after
            myBody.AddForce(Physics.gravity * myBody.mass);
            myArmor.transform.rotation = _waypoints[_currentWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }

    private void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        if (relativeWaypointPos.sqrMagnitude < _proxSqr)
        {
            _currentWaypoint += 1;

            if (_currentWaypoint == _waypoints.Length)
            {
                _currentWaypoint = 0;
            }
        }
    }

    private void GetWaypoints()
    {
        var potentialWaypoints = theWaypointContainer.GetComponentsInChildren<Transform>();
        _waypoints = new Transform[potentialWaypoints.Length - 1];

        for (int i = 1; i < potentialWaypoints.Length; i++)
        {
            _waypoints[i - 1] = potentialWaypoints[i];
        }
    }

    public Transform GetLastWaypoint()
    {
        if (_currentWaypoint - 1 < 0)
        {
            return _waypoints[_waypoints.Length - 1];
        }
        else
        {
            return _waypoints[_currentWaypoint - 1];
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;
    }
}
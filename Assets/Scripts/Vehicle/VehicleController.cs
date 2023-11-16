using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private GameObject WaypointContainer;
    [SerializeField] private VehicleData Vehicle;
    [SerializeField] private GameObject VehicleArmor;
    [SerializeField] private ParticleSystem Exhaust;
    [SerializeField] private ParticleSystem Thruster;
    [SerializeField] private Light[] TailLights;

    private Transform[] _waypoints;
    private int _currentWaypoint;
    private Rigidbody _vehicleBody;
    
    public Transform GetCurrentWaypoint => _waypoints[_currentWaypoint];

    private void Awake()
    {
        _vehicleBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GetWaypoints();
        _currentWaypoint = 1;
        //_proxSqr = _aiCarConfig.WaypointProx * _aiCarConfig.WaypointProx;
    }

    private void Update()
    {
        var waypointPosition = _waypoints[_currentWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));
        var localVelocity = transform.InverseTransformDirection(_vehicleBody.velocity);

        _vehicleBody.AddForce(VehicleArmor.transform.forward * Vehicle.GetRollSpeed, ForceMode.Force);
        _vehicleBody.AddForce(Physics.gravity * _vehicleBody.mass);
    }

    private void GetWaypoints()
    {
        Transform[] potentialWaypoints = WaypointContainer.GetComponentsInChildren<Transform>();
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
}

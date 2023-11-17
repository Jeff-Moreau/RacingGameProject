using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private GameObject WaypointContainer;
    [SerializeField] private VehicleData Vehicle;
    [SerializeField] private GameObject VehicleArmor;
    [SerializeField] private ParticleSystem Exhaust;
    [SerializeField] private ParticleSystem Thruster;
    [SerializeField] private Light[] TailLights;
    [SerializeField] private RaceManager ManageRace;
    [SerializeField] private LoadingManager LoadManager;

    private Transform[] _waypoints;
    private int _currentWaypoint;
    private Rigidbody _vehicleBody;
    private float _proxSqr;

    public Transform GetCurrentWaypoint => _waypoints[_currentWaypoint];

    private void Awake()
    {
        _vehicleBody = GetComponent<Rigidbody>();
    }

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

        if (ManageRace.GameStarted)
        {
            _vehicleBody.AddForce(VehicleArmor.transform.forward * 50, ForceMode.Force); // change after
            _vehicleBody.AddForce(Physics.gravity * _vehicleBody.mass);
            VehicleArmor.transform.rotation = _waypoints[_currentWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }

    private void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        /*Debug.Log(_currentWaypoint + " " + relativeWaypointPos.sqrMagnitude);
        Debug.Log(_proxSqr);*/
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FinishLine")
        {
            if (ManageRace.GetRacers < LoadManager.RaceCount)
            {
                ManageRace.AddRacers(gameObject);
            }
            else if (ManageRace.GetRacers >= LoadManager.RaceCount)
            {
                ManageRace.ResetRacers();
                ManageRace.AddRacers(gameObject);
            }
        }
    }
}

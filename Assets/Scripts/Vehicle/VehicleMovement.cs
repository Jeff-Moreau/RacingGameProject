using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private VehicleData Vehicle;
    [SerializeField] private GameObject VehicleArmor;
    [SerializeField] private ParticleSystem Exhaust;
    [SerializeField] private ParticleSystem Thruster;
    [SerializeField] private Light[] TailLights;
    [SerializeField] private RaceManager ManageRace;
    [SerializeField] private Collider FinishLine;
    [SerializeField] private LoadingManager LoadManager;
    [SerializeField] private GameObject WaypointContainer;

    private Rigidbody _vehicleBody;
    private int _currentPosition;
    private Transform[] _waypoints;
    private int _currentWaypoint;
    private float _proxSqr;

    public int CurrentPosition => _currentPosition;

    private void Awake()
    {
        _vehicleBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _currentPosition = 0;
        GetWaypoints();
        _currentWaypoint = 14;
        _proxSqr = 8 * 8; // change after
    }

    private void Update()
    {
        if (ManageRace.GameStarted && !ManageRace.RaceOver)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vehicle.SetIsMoving(true);
                Exhaust.gameObject.SetActive(false);
                Thruster.gameObject.SetActive(true);

                for (int i = 0; i < TailLights.Length; i++)
                {
                    TailLights[i].gameObject.SetActive(false);
                }
                _vehicleBody.AddForce(VehicleArmor.transform.forward * Vehicle.GetRollSpeed, ForceMode.Force);
                _vehicleBody.AddForce(Physics.gravity * _vehicleBody.mass);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                Vehicle.SetIsMoving(false);
                Exhaust.gameObject.SetActive(true);
                Thruster.gameObject.SetActive(false);

                for (int i = 0; i < TailLights.Length; i++)
                {
                    TailLights[i].gameObject.SetActive(true);
                }
                _vehicleBody.AddForce(Physics.gravity * (_vehicleBody.mass * 10));
            }

            if (Input.GetKey(KeyCode.D))
            {
                VehicleArmor.transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                VehicleArmor.transform.Rotate(new Vector3(0, -70, 0) * Time.deltaTime);
            }
        }
        else
        {
            var waypointPosition = _waypoints[_currentWaypoint].position;
            var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

            if (ManageRace.GameStarted)
            {
                _vehicleBody.AddForce(VehicleArmor.transform.forward * 25, ForceMode.Force); // change after
                _vehicleBody.AddForce(Physics.gravity * _vehicleBody.mass);
                VehicleArmor.transform.rotation = _waypoints[_currentWaypoint].rotation;
            }
            CheckWaypointPosition(relativeWaypointPos);
        }
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
        var potentialWaypoints = WaypointContainer.GetComponentsInChildren<Transform>();
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
        if (other.name == "FinishLine" && !ManageRace.RaceOver)
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
            _currentPosition = ManageRace.GetRacers;
            ManageRace.SetCurrentLap(1);
        }
    }
}
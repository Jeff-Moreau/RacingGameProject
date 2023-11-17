using TMPro;
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

    private Rigidbody _vehicleBody;
    private int _currentPosition;

    public int CurrentPosition => _currentPosition;

    private void Awake()
    {
        _vehicleBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _currentPosition = 1;
    }

    private void Update()
    {
        if (ManageRace.GameStarted)
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
            _currentPosition = ManageRace.GetRacers;
            ManageRace.SetCurrentLap(1);
        }
    }
}

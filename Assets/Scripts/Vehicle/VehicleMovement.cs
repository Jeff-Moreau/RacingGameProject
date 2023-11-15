using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private VehicleData Vehicle;
    [SerializeField] private GameObject VehicleArmor;
    [SerializeField] private ParticleSystem Exhaust;
    [SerializeField] private ParticleSystem Thruster;
    [SerializeField] private Light[] TailLights;

    private Rigidbody _vehicleBody;

    private void Awake()
    {
        _vehicleBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var horMovement = Input.GetAxis("Horizontal");
        var verMovement = Input.GetAxis("Vertical");
        var movement = new Vector3(horMovement, 0, verMovement);

        if (Input.GetKey(KeyCode.W))
        {
            _vehicleBody.AddForce(VehicleArmor.transform.forward * Vehicle.GetRollSpeed, ForceMode.Force);
            _vehicleBody.AddForce(Physics.gravity*_vehicleBody.mass);
        }

        if (Input.GetKey(KeyCode.D))
        {
            VehicleArmor.transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            VehicleArmor.transform.Rotate(new Vector3(0, -70, 0) * Time.deltaTime);
        }

        if (horMovement == 0 && verMovement == 0)
        {
            Vehicle.SetIsMoving(false);
            Exhaust.gameObject.SetActive(true);
            Thruster.gameObject.SetActive(false);

            for (int i = 0; i < TailLights.Length; i++)
            {
                TailLights[i].gameObject.SetActive(true);
            }

        }
        else
        {
            Vehicle.SetIsMoving(true);
            Exhaust.gameObject.SetActive(false);
            Thruster.gameObject.SetActive(true);

            for (int i = 0; i < TailLights.Length; i++)
            {
                TailLights[i].gameObject.SetActive(false);
            }
        }
    }
}

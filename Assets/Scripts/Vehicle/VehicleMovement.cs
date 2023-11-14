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

        _vehicleBody.AddForce(movement * Vehicle.GetRollSpeed);
        VehicleArmor.transform.LookAt(movement + VehicleArmor.transform.position);

        if (horMovement == 0 && verMovement == 0)
        {
            Vehicle.SetIsMoving(false);
            Exhaust.gameObject.SetActive(true);
            Thruster.gameObject.SetActive(false);

            for(int i = 0; i < TailLights.Length; i++)
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

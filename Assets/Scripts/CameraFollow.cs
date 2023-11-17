using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Ball;
    [SerializeField] private GameObject VehicleArmor;

    private void Update()
    {
        transform.position = new Vector3(VehicleArmor.transform.position.x, VehicleArmor.transform.position.y, VehicleArmor.transform.position.z - 20);
        transform.rotation = VehicleArmor.transform.rotation;
    }
}

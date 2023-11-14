using UnityEngine;

public class FollowBall : MonoBehaviour
{
    [SerializeField] private Transform Ball;
    [SerializeField] private VehicleData ArmorOffset;

    private void Update()
    {
        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y + ArmorOffset.GetArmorHeight, Ball.transform.position.z);
    }
}
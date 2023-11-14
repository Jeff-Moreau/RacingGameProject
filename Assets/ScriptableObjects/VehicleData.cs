using UnityEngine;

[CreateAssetMenu(fileName = "VehicleData", menuName = "ScriptableObject/VehicleData")]
public class VehicleData : ScriptableObject
{
    [SerializeField] private float _rollSpeed = 0;
    [SerializeField] private float _armorHeight = 0;
    [SerializeField] private AudioClip _thrusterSound = null;
    [SerializeField] private AudioClip _idleSound = null;

    private bool _isMoving = false;

    public float GetRollSpeed => _rollSpeed;
    public float GetArmorHeight => _armorHeight;
    public AudioClip GetThrusterSound => _thrusterSound;
    public AudioClip GetIdleSound => _idleSound;
    public bool GetIsMoving => _isMoving;
    public bool SetIsMoving(bool yesno) => _isMoving = yesno;
}

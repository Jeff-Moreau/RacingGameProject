using UnityEngine;

[CreateAssetMenu(fileName = "VehicleData", menuName = "ScriptableObject/VehicleData")]
public class VehicleData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float theRollSpeed = 0;

    [Header("Armor Height Offset above Ball")]
    [SerializeField] private float theArmorHeight = 0;

    [Header("Sound FX")]
    [SerializeField] private AudioClip theIdleSound = null;
    [SerializeField] private AudioClip theThrusterSound = null;

    public bool GetIsMoving => isMoving;
    public float GetRollSpeed => theRollSpeed;

    public float GetArmorHeight => theArmorHeight;

    public AudioClip GetIdleSound => theIdleSound;
    public AudioClip GetThrusterSound => theThrusterSound;

    public void SetIsMoving(bool yesno) => isMoving = yesno;
}

using UnityEngine;

[CreateAssetMenu(fileName = "VehicleData", menuName = "ScriptableObject/VehicleData")]
public class VehicleData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float theRollSpeed = 0;
    [SerializeField] private float theMassMultiplier = 0;
    [SerializeField] private float theRotationSpeed = 0;
    [SerializeField] private float theWaypointProximity = 0;

    [Header("Armor Height Offset above Ball")]
    [SerializeField] private float theArmorHeight = 0;

    [Header("Sound FX")]
    [SerializeField] private AudioClip theIdleSound = null;
    [SerializeField] private AudioClip theThrusterSound = null;

    [Header("Ball Materials")]
    [SerializeField] private Material[] theBallMaterials = null;

    // GETTERS
    public bool GetIsMoving => isMoving;
    public float GetRollSpeed => theRollSpeed;
    public float GetMassMultiplier => theMassMultiplier;
    public float GetRotationSpeed => theRotationSpeed;
    public float GetWaypointProximity => theWaypointProximity;
    public float GetArmorHeight => theArmorHeight;
    public AudioClip GetIdleSound => theIdleSound;
    public AudioClip GetThrusterSound => theThrusterSound;
    public Material[] GetBallMaterials => theBallMaterials;

    // SETTERS
    public void SetIsMoving(bool yesno) => isMoving = yesno;
}
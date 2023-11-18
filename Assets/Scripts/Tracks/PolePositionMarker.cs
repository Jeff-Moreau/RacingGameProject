using UnityEngine;

public class PolePositionMarker : MonoBehaviour
{
    private bool isSpotTaken;

    public bool GetSpotTaken => isSpotTaken;
    public bool SetSpotTaken(bool spotTaken) => isSpotTaken = spotTaken;
}

using UnityEngine;

public class PolePositionMarker : MonoBehaviour
{
    private bool isSpotTaken = false;

    public bool GetSpotTaken => isSpotTaken;
    public bool SetSpotTaken(bool spotTaken) => isSpotTaken = spotTaken;
}

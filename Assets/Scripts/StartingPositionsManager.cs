using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPositionsManager : MonoBehaviour
{
    private bool _spotTaken;

    public bool GetSpotTaken => _spotTaken;
    public bool SetSpotTaken(bool spotTaken) => _spotTaken = spotTaken;
}

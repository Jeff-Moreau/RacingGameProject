using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _aiVehicles;
    [SerializeField] private GameObject[] _polePositions;
    [SerializeField, Range(1, 19)] private int _aiCount;

    private void Start()
    {
        var totalCount = 0;
        var randomSpot = Random.Range(0, _aiCount);


        while (totalCount < _aiCount)
        {
            for (int i = 0; i < _polePositions.Length; i++)
            {
                if (_polePositions[randomSpot].gameObject.GetComponent<StartingPositionsManager>().GetSpotTaken == false && totalCount < _aiCount)
                {
                    _aiVehicles[i].transform.position = _polePositions[randomSpot].transform.position;
                    _aiVehicles[i].transform.rotation = _polePositions[randomSpot].transform.rotation;
                    _polePositions[randomSpot].gameObject.GetComponent<StartingPositionsManager>().SetSpotTaken(true);
                    totalCount++;
                }
            }
        }
    }
}

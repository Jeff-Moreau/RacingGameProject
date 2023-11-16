using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerVehicle;
    [SerializeField] private GameObject[] _aiVehicles;
    [SerializeField] private GameObject[] _polePositions;
    [SerializeField, Range(1, 19)] private int _aiCount;

    private void Start()
    {
        var totalCount = 0;
        var randomPosition = Random.Range(0, _aiCount);

        _playerVehicle.transform.position = _polePositions[randomPosition].transform.position;
        _playerVehicle.transform.rotation = _polePositions[randomPosition].transform.rotation;
        _polePositions[randomPosition].gameObject.GetComponent<StartingPositionsManager>().SetSpotTaken(true);

        for (int i = 0; i < _polePositions.Length; i++)
        {
            if (_polePositions[i].gameObject.GetComponent<StartingPositionsManager>().GetSpotTaken == false && totalCount <= _aiCount)
            {
                _aiVehicles[i].transform.position = _polePositions[i].transform.position;
                _aiVehicles[i].transform.rotation = _polePositions[i].transform.rotation;
                _polePositions[i].gameObject.GetComponent<StartingPositionsManager>().SetSpotTaken(true);
                totalCount++;
            }
        }
    }
}

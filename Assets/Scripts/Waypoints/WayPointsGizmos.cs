using UnityEngine;

public class WayPointsGizmos : MonoBehaviour
{
    private const string WAYPOINT = "Waypoint ";

    [SerializeField] private float _waypointSize = 1.0f;
    [SerializeField] private Color _waypointColor = Color.red;

    private Transform[] _wayPoints = null;

    private void OnDrawGizmos()
    {
        _wayPoints = GetComponentsInChildren<Transform>();
        var lastWaypoint = _wayPoints[_wayPoints.Length - 1].position;

        for (int i = 1; i < _wayPoints.Length; i++)
        {
            Gizmos.color = _waypointColor;
            Gizmos.DrawSphere(_wayPoints[i].position, _waypointSize);
            Gizmos.DrawLine(lastWaypoint, _wayPoints[i].position);
            lastWaypoint = _wayPoints[i].position;

            _wayPoints[i].name = WAYPOINT + i.ToString();
        }
    }
}

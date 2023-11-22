using UnityEngine;

[CreateAssetMenu(fileName = "TrackData", menuName = "ScriptableObject/TrackData")]
public class TrackData : ScriptableObject
{
    [Header("Track Information")]
    [SerializeField] private string myName = "";
    [SerializeField] private int myLaps = 0;

    public string GetName => myName;
    public int GetLaps => myLaps;

    public void Start()
    {
        myName = "Johnny";
    }
}

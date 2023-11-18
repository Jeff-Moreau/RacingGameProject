using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static LoadingManager myInstance;
    private void Singleton()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            myInstance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public static LoadingManager Load => myInstance;
    // SINGLETON ENDS

    [Header("Loading Data")]
    [SerializeField, Range(1, 19)] private int myVehicleCount;

    [Header("Other Data Needed")]
    [SerializeField] private GameObject thePlayerVehicle;
    [SerializeField] private GameObject[] theAIVehicles;
    [SerializeField] private GameObject[] thePolePositions;

    private PolePositionMarker[] thePolePositionsTaken;

    public int RaceCount => myVehicleCount + 1;

    private void Awake()
    {
        Singleton();

        for (int i = 0; i < thePolePositions.Length; i++)
        {
            thePolePositionsTaken[i] = thePolePositions[i].gameObject.GetComponent<PolePositionMarker>();
        }
    }

    private void Start()
    {
        var totalCount = 0;
        var randomPolePosition = Random.Range(0, myVehicleCount);

        thePlayerVehicle.transform.position = thePolePositions[randomPolePosition].transform.position;
        thePlayerVehicle.transform.rotation = thePolePositions[randomPolePosition].transform.rotation;
        thePolePositionsTaken[randomPolePosition].SetSpotTaken(true);

        for (int i = 0; i < thePolePositions.Length; i++)
        {
            if (thePolePositionsTaken[i].GetSpotTaken == false && totalCount < myVehicleCount)
            {
                theAIVehicles[i].transform.position = thePolePositions[i].transform.position;
                theAIVehicles[i].transform.rotation = thePolePositions[i].transform.rotation;
                thePolePositionsTaken[i].SetSpotTaken(true);
                theAIVehicles[i].SetActive(true);
                totalCount++;
            }
        }
    }
}

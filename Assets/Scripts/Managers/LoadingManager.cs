using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    }
    public static LoadingManager Load => myInstance;
    // SINGLETON ENDS

    [Header("Loading Data")]
    [SerializeField, Range(1, 20)] private int myVehicleCount; // change based on track and players not a solid number

    [Header("Other Data Needed")]
    [SerializeField] private GameObject[] thePlayerVehicles;
    [SerializeField] private AIVehiclePool theAIVehiclePool;
    [SerializeField] private GameObject[] theTrack; // fix later with list of tracks
    
    private GameObject[] thePolePositions;

    public int RaceCount => myVehicleCount;// check this later might be wrong should check players as well
    public GameObject GetCurrentTrack => theTrack[0];

    private void Awake()
    {
        Singleton();

        thePolePositions = theTrack[0].GetComponent<TrackInformation>().GetPolePositions;
        theAIVehiclePool.SetTotalAIVehciles(thePolePositions.Length - thePlayerVehicles.Length);
    }

    private void Start()
    {
        Instantiate(theTrack[0]);
        var totalCount = 0;
        var randomPolePosition = Random.Range(0, myVehicleCount);

        for (int i = 0; i < thePolePositions.Length; i++)
        {
            thePolePositions[i].GetComponent<PolePositionMarker>().SetSpotTaken(false);
        }

        for (int i = 0; i < thePolePositions.Length; i++)
        {
            if (thePolePositions[i].GetComponent<PolePositionMarker>().GetSpotTaken == false && totalCount < myVehicleCount)
            {
                for (int j = 0; j < thePlayerVehicles.Length; j++)
                {
                    if (thePolePositions[randomPolePosition].GetComponent<PolePositionMarker>().GetSpotTaken == false && totalCount < myVehicleCount)
                    {
                        thePlayerVehicles[j].transform.position = thePolePositions[randomPolePosition].transform.position;
                        thePlayerVehicles[j].transform.rotation = thePolePositions[randomPolePosition].transform.rotation;
                        thePolePositions[randomPolePosition].GetComponent<PolePositionMarker>().SetSpotTaken(true);
                        thePlayerVehicles[j].SetActive(true);
                        totalCount++;
                    }
                }

                Debug.Log(thePolePositions[randomPolePosition].GetComponent<PolePositionMarker>().GetSpotTaken);
                var newAIVehicle = theAIVehiclePool.GetAIVehicle();

                if (newAIVehicle != null)
                {
                    newAIVehicle.transform.position = thePolePositions[i].transform.position;
                    newAIVehicle.transform.rotation = thePolePositions[i].transform.rotation;
                    thePolePositions[i].GetComponent<PolePositionMarker>().SetSpotTaken(true);
                    totalCount++;
                }

                for (int j = 0; j < theAIVehiclePool.GetAIVehicleList.Count; j++)
                {
                    newAIVehicle.SetActive(true);
                }
            }
        }
    }
}

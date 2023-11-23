using UnityEngine;

public class AIController : VehicleController
{
    // INSPECTOR VARIABLES
    [Header("Scriptable Object Data")]
    [SerializeField] protected VehicleData myData = null;

    // LOCAL VARIABLE
    private float theVehicleSpeed;

    private void Start()
    {
        InitializeVariables();
        GetWaypoints();
    }

    private void Update()
    {
        AutoDrive();
    }

    private void InitializeVariables()
    {
        SetExhaustParticles();
        SetArmorType();


        theVehicleSpeed = myData.GetRollSpeed;
        myCurrentTrackWaypoint = 0;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;
    }

    private void SetExhaustParticles()
    {
        for (int i = 0; i < myThrusterParticles.Length; i++)
        {
            myThrusterParticles[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < myExhaustParticles.Length; i++)
        {
            myExhaustParticles[i].gameObject.SetActive(false);
        }
    }

    private void SetArmorType()
    {
        myArmorType = Random.Range(0, 3);

        if (myArmorType == 0)
        {
            myArmor[0].SetActive(true);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(false);
            myRenderer.material = myData.GetBallMaterials[Random.Range(0, myData.GetBallMaterials.Length)];
            myRenderer.material.SetColor("_Color", Color.red);
            myExhaustParticles[0].gameObject.SetActive(true);
        }
        else if (myArmorType == 1)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(true);
            myArmor[2].SetActive(false);
            myRenderer.material = myData.GetBallMaterials[Random.Range(0, myData.GetBallMaterials.Length)];
            myRenderer.material.SetColor("_Color", Color.red);
            myExhaustParticles[1].gameObject.SetActive(true);
            myExhaustParticles[2].gameObject.SetActive(true);
        }
        else if (myArmorType == 2)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(true);
            myRenderer.material = myData.GetBallMaterials[Random.Range(0, myData.GetBallMaterials.Length)];
            myRenderer.material.SetColor("_Color", Color.red);
            myExhaustParticles[3].gameObject.SetActive(true);
        }
    }

    private void AutoDrive()
    {
        var waypointPosition = theTrackWaypointsToFollow[myCurrentTrackWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));
        
        if (RaceManager.Load.GetPlayerPosition <= 20 && RaceManager.Load.GetPlayerPosition >= 4)
        {
            theVehicleSpeed = Random.Range(myData.GetRollSpeed * 0.5f, myData.GetRollSpeed);
        }
        else if (RaceManager.Load.GetPlayerPosition == 1)
        {
            theVehicleSpeed = myData.GetRollSpeed * 1.3f;
        }
        else
        {
            theVehicleSpeed = Random.Range(myData.GetRollSpeed, myData.GetRollSpeed * 1.3f);
        }

        if (RaceManager.Load.GetGameStarted)
        {
            if (myArmor[0].activeInHierarchy)
            {
                myExhaustParticles[0].gameObject.SetActive(false);
                myThrusterParticles[0].gameObject.SetActive(true);
            }
            else if (myArmor[1].activeInHierarchy)
            {
                myExhaustParticles[1].gameObject.SetActive(false);
                myExhaustParticles[2].gameObject.SetActive(false);
                myThrusterParticles[1].gameObject.SetActive(true);
                myThrusterParticles[2].gameObject.SetActive(true);
            }
            else if (myArmor[2].activeInHierarchy)
            {
                myExhaustParticles[3].gameObject.SetActive(false);
                myThrusterParticles[3].gameObject.SetActive(true);
            }

            mySphere.AddForce(myArmor[myArmorType].transform.forward * theVehicleSpeed, ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor[myArmorType].transform.LookAt(theTrackWaypointsToFollow[myCurrentTrackWaypoint]);
            //myArmor[myArmorType].transform.rotation = theTrackWaypointsToFollow[myCurrentTrackWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FinishLine" && !RaceManager.Load.GetRaceOver)
        {
            if (RaceManager.Load.GetRacers < LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.AddRacers(gameObject);
            }
            else if (RaceManager.Load.GetRacers >= LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.ResetRacers();
                RaceManager.Load.AddRacers(gameObject);
            }
        }
    }
}
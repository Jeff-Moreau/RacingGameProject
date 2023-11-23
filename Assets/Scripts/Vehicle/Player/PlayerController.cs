using UnityEngine;

public class PlayerController : VehicleController
{
    // INSPECTOR VARIABLES
    [Header("Scriptable Object Data")]
    [SerializeField] protected VehicleData myData = null;

    private void Start()
    {
        InitializeVariables();
        GetWaypoints();
    }

    private void Update()
    {
        if (RaceManager.Load.GetGameStarted && !RaceManager.Load.GetRaceOver)
        {
            PressMoveForwardKey();
            ReleaseMoveForwardKey();
            PressRotateRightKey();
            PressRotateLeftKey();
        }
        else
        {
            AutoDriveTakeOver();
        }
    }

    private void InitializeVariables()
    {
        for (int i = 0; i < myThrusterParticles.Length; i++)
        {
            myThrusterParticles[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < myExhaustParticles.Length; i++)
        {
            myExhaustParticles[i].gameObject.SetActive(false);
        }

        Debug.Log(myExhaustParticles.Length);

        SetArmorType();

        myRenderer.material = myBallMaterial;
        myRenderer.material.SetColor("_Color", new Color(myMaterialRed, myMaterialGreen, myMaterialBlue, myMaterialAlpha));
        myCurrentRacePosition = 0;
        myCurrentTrackWaypoint = 0;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;
        myData.SetIsMoving(false);
    }

    private void SetArmorType()
    {
        if (myArmorType == 0)
        {
            myArmor[0].SetActive(true);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(false);
            myExhaustParticles[0].gameObject.SetActive(true);
        }
        else if (myArmorType == 1)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(true);
            myArmor[2].SetActive(false);
            myExhaustParticles[1].gameObject.SetActive(true);
            myExhaustParticles[2].gameObject.SetActive(true);
        }
        else if (myArmorType == 2)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(true);
            myExhaustParticles[3].gameObject.SetActive(true);
        }
    }

    private void PressRotateLeftKey()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myArmor[myArmorType].transform.Rotate(new Vector3(0, -myData.GetRotationSpeed, 0) * Time.deltaTime);
        }
    }

    private void PressRotateRightKey()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myArmor[myArmorType].transform.Rotate(new Vector3(0, myData.GetRotationSpeed, 0) * Time.deltaTime);
        }
    }

    private void ReleaseMoveForwardKey()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            myData.SetIsMoving(false);

            if (myArmor[0].activeInHierarchy)
            {
                myExhaustParticles[0].gameObject.SetActive(true);
                myThrusterParticles[0].gameObject.SetActive(false); 
            }
            else if (myArmor[1].activeInHierarchy)
            {
                myExhaustParticles[1].gameObject.SetActive(true);
                myExhaustParticles[2].gameObject.SetActive(true);
                myThrusterParticles[1].gameObject.SetActive(false);
                myThrusterParticles[2].gameObject.SetActive(false);
            }
            else if (myArmor[2].activeInHierarchy)
            {
                myExhaustParticles[3].gameObject.SetActive(true);
                myThrusterParticles[3].gameObject.SetActive(false);
            }

            mySphere.AddForce(Physics.gravity * (mySphere.mass * myData.GetMassMultiplier));
        }
    }

    private void PressMoveForwardKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myData.SetIsMoving(true);

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

            mySphere.AddForce(myArmor[myArmorType].transform.forward * myData.GetRollSpeed, ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
        }
    }

    private void AutoDriveTakeOver()
    {
        var waypointPosition = theTrackWaypointsToFollow[myCurrentTrackWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        myData.SetIsMoving(false);

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

            mySphere.AddForce(myArmor[myArmorType].transform.forward * (myData.GetRollSpeed / 2), ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor[myArmorType].transform.rotation = theTrackWaypointsToFollow[myCurrentTrackWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }
}
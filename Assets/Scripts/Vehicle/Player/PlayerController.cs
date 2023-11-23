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
        if (myArmorType == 0)
        {
            myArmor[0].SetActive(true);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(false);
        }
        else if (myArmorType == 1)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(true);
            myArmor[2].SetActive(false);
        }
        else if (myArmorType == 2)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(true);
        }

        myRenderer.material = myBallMaterial;
        myRenderer.material.SetColor("_Color", new Color(myMaterialRed, myMaterialGreen, myMaterialBlue, myMaterialAlpha));
        myCurrentRacePosition = 0;
        myCurrentTrackWaypoint = 14;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;
        myData.SetIsMoving(false);
        for (int i = 0; i < myThrusterParticles.Length; i++)
        {
            myThrusterParticles[i].gameObject.SetActive(false);
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

            for (int i = 0; i < myExhaustParticles.Length; i++)
            {
                myExhaustParticles[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < myThrusterParticles.Length; i++)
            {
                myThrusterParticles[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < myTailLightBulbs.Length; i++)
            {
                myTailLightBulbs[i].gameObject.SetActive(true);
            }

            mySphere.AddForce(Physics.gravity * (mySphere.mass * myData.GetMassMultiplier));
        }
    }

    private void PressMoveForwardKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myData.SetIsMoving(true);

            for (int i = 0; i < myExhaustParticles.Length; i++)
            {
                myExhaustParticles[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < myThrusterParticles.Length; i++)
            {
                myThrusterParticles[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < myTailLightBulbs.Length; i++)
            {
                myTailLightBulbs[i].gameObject.SetActive(false);
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
            for (int i = 0; i < myThrusterParticles.Length; i++)
            {
                myThrusterParticles[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < myExhaustParticles.Length; i++)
            {
                myExhaustParticles[i].gameObject.SetActive(true);
            }

            mySphere.AddForce(myArmor[myArmorType].transform.forward * (myData.GetRollSpeed / 2), ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor[myArmorType].transform.rotation = theTrackWaypointsToFollow[myCurrentTrackWaypoint].rotation;
        }
        CheckWaypointPosition(relativeWaypointPos);
    }
}
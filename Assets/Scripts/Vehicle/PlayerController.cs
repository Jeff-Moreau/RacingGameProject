using UnityEngine;

public class PlayerController : VehicleController
{
    private float myRotationSpeed;
    private float myMassMultiplier;

    private void Start()
    {
        myMassMultiplier = 15;
        myRotationSpeed = 70;
        myCurrentPosition = 0;
        myCurrentWaypoint = 14;
        myData.SetIsMoving(false);
        GetWaypoints();
    }

    private void Update()
    {
        if (RaceManager.Load.GameStarted && !RaceManager.Load.RaceOver)
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

                mySphere.AddForce(myArmor.transform.forward * myData.GetRollSpeed, ForceMode.Force);
                mySphere.AddForce(Physics.gravity * mySphere.mass);
            }
            else if (Input.GetKeyUp(KeyCode.W))
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

                mySphere.AddForce(Physics.gravity * (mySphere.mass * myMassMultiplier));
            }

            if (Input.GetKey(KeyCode.D))
            {
                myArmor.transform.Rotate(new Vector3(0, myRotationSpeed, 0) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                myArmor.transform.Rotate(new Vector3(0, -myRotationSpeed, 0) * Time.deltaTime);
            }
        }
        else
        {
            var waypointPosition = myWaypoints[myCurrentWaypoint].position;
            var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

            myData.SetIsMoving(false);

            if (RaceManager.Load.GameStarted)
            {
                mySphere.AddForce(myArmor.transform.forward * (myData.GetRollSpeed / 2), ForceMode.Force);
                mySphere.AddForce(Physics.gravity * mySphere.mass);
                myArmor.transform.rotation = myWaypoints[myCurrentWaypoint].rotation;
            }
            CheckWaypointPosition(relativeWaypointPos);
        }
    }
}
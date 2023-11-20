using System.Collections.Generic;
using UnityEngine;

public class AIVehiclePool : MonoBehaviour
{
    [SerializeField] private GameObject myAIVehicle;

    private List<GameObject> myAIVehicleList;
    private int myTotalAIVehiclesNeeded;

    public List<GameObject> GetAIVehicleList => myAIVehicleList;
    public int SetTotalAIVehcilesNeeded(int needed) => myTotalAIVehiclesNeeded;

    private void Start()
    {
        myTotalAIVehiclesNeeded = 0;
        myAIVehicleList = new List<GameObject>();


        for (int i = 0; i < myTotalAIVehiclesNeeded; i++)
        {
            myAIVehicleList.Add(Instantiate(myAIVehicle, transform));
            myAIVehicleList[i].SetActive(false);
        }
    }

    public GameObject GetAIVehicle()
    {
        for (int i = 0; i < myTotalAIVehiclesNeeded; i++)
        {
            if (!myAIVehicleList[i].activeInHierarchy)
            {
                return myAIVehicleList[i];
            }
        }

        var newAIVehicle = Instantiate(myAIVehicle, transform);
        myAIVehicle.SetActive(false);
        myAIVehicleList.Add(newAIVehicle);

        return newAIVehicle;
    }
}
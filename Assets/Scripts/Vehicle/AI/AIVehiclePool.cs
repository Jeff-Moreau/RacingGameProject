using System.Collections.Generic;
using UnityEngine;

public class AIVehiclePool : MonoBehaviour
{
    [SerializeField] private GameObject myAIVehicle;

    private List<GameObject> myAIVehicleList;
    private int myTotalAIVehicles;

    public List<GameObject> GetAIVehicleList => myAIVehicleList;

    private void Start()
    {
        myAIVehicleList = new List<GameObject>();

        myTotalAIVehicles = 25;

        for (int i = 0; i < myTotalAIVehicles; i++)
        {
            myAIVehicleList.Add(Instantiate(myAIVehicle, transform));
            myAIVehicleList[i].SetActive(false);
        }
    }

    public GameObject GetAIVehicle()
    {
        for (int i = 0; i < myTotalAIVehicles; i++)
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
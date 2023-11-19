using System.Collections.Generic;
using UnityEngine;

public class AIVehiclePool : MonoBehaviour
{
    [SerializeField] private GameObject myAIVehicle;

    private List<GameObject> myAIVehicleList;
    private int myTotalAIVehicles;

    public List<GameObject> GetAIVehicleList => myAIVehicleList;
    public int SetTotalAIVehciles(int amount) => myTotalAIVehicles = amount;

    private void Start()
    {
        myAIVehicleList = new List<GameObject>();

        Debug.Log("Pool " + myTotalAIVehicles);

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
        myAIVehicle.gameObject.SetActive(false);
        myAIVehicleList.Add(newAIVehicle);

        return newAIVehicle;
    }
}

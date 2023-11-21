using System.Collections.Generic;
using UnityEngine;

public class AIVehiclePool : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [Header("Prefab to Pool")]
    [SerializeField] private GameObject thePrefab = null;

    // LOCAL VARIABLES
    private List<GameObject> myPrefabList;
    private int myTotalPrefabsNeeded;

    // GETTERS
    public List<GameObject> GetPrefabList => myPrefabList;

    // SETTERS
    public int SetTotalPrefabsNeeded(int needed) => myTotalPrefabsNeeded;

    private void Start()
    {
        InitializeVariables();
        FillListWithPrefab();
    }

    private void InitializeVariables()
    {
        myPrefabList = new List<GameObject>();
        myTotalPrefabsNeeded = 0;
    }

    private void FillListWithPrefab()
    {
        for (int i = 0; i < myTotalPrefabsNeeded; i++)
        {
            myPrefabList.Add(Instantiate(thePrefab, transform));
            myPrefabList[i].SetActive(false);
        }
    }

    public GameObject GetAvailableAIVehicle()
    {
        for (int i = 0; i < myTotalPrefabsNeeded; i++)
        {
            if (!myPrefabList[i].activeInHierarchy)
            {
                return myPrefabList[i];
            }
        }

        return IfFullAddOneMore();
    }

    private GameObject IfFullAddOneMore()
    {
        var extraPrefab = Instantiate(thePrefab, transform);
        thePrefab.SetActive(false);
        myPrefabList.Add(extraPrefab);

        return extraPrefab;
    }
}
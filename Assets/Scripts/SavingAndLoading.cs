using System.IO;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private BallEditor theEditor;

    private VehicleController myPlayer;
    private string mySaveFilePath;

    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        myPlayer = thePlayer.GetComponentInChildren<VehicleController>();
    }

    public void SaveGame()
    {
        myPlayer.SetName("Jeff");
        myPlayer.SetBallMaterial(theEditor.GetMaterial);
        myPlayer.SetMaterialRed(theEditor.GetMaterialRed);
        myPlayer.SetMaterialGreen(theEditor.GetMaterialGreen);
        myPlayer.SetMaterialBlue(theEditor.GetMaterialBlue);
        myPlayer.SetMaterialAlpha(theEditor.GetMaterialAlpha);
        mySaveFilePath = Application.persistentDataPath + "/VehicleData-" + myPlayer.GetName + ".json";
        var savePlayerData = JsonUtility.ToJson(myPlayer);
        File.WriteAllText(mySaveFilePath, savePlayerData);

        Debug.Log("Saved Vehicle at : " + mySaveFilePath);
    }

    public void LoadGame()
    {
        mySaveFilePath = Application.persistentDataPath + "/VehicleData-Default.json";

        if (File.Exists(mySaveFilePath))
        {
            var loadVehicleData = File.ReadAllText(mySaveFilePath);
            JsonUtility.FromJsonOverwrite(loadVehicleData, myPlayer);

            Debug.Log("Loaded Vehicle!");
        }
        else
        {
            Debug.Log("No Vehicle Found!");
        }
    }

    public void DeleteVehicle()
    {
        if (File.Exists(mySaveFilePath))
        {
            File.Delete(mySaveFilePath);

            Debug.Log("Vehicle Deleted");
        }
        else
        {
            Debug.Log("No Vehicle Found");
        }
    }
}
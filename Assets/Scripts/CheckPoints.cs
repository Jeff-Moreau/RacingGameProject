using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!RaceManager.Load.RaceOver)
        {
            if (RaceManager.Load.GetRacers < LoadingManager.Load.RaceCount)
            {
                RaceManager.Load.AddRacers(other.gameObject);
            }
            else if (RaceManager.Load.GetRacers >= LoadingManager.Load.RaceCount)
            {
                RaceManager.Load.ResetRacers();
                RaceManager.Load.AddRacers(other.gameObject);
            }
        }
    }
}

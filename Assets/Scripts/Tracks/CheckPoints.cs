using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!RaceManager.Load.RaceOver)
        {
            if (RaceManager.Load.GetRacers < LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.AddRacers(other.gameObject);

                if (other.gameObject.CompareTag("Player"))
                {
                    RaceManager.Load.SetPlayerPosition(RaceManager.Load.GetRacers);
                }
            }
            else if (RaceManager.Load.GetRacers >= LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.ResetRacers();
                RaceManager.Load.AddRacers(other.gameObject);
            }
        }
    }
}

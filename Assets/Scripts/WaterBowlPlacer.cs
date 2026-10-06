using UnityEngine;

public class WaterBowlPlacer : TimedObjectPlacer
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        MinimumSecondsToWait = GameParameters.WaterBowlMinimumSecondsToWait;
        MaximumSecondsToWait = GameParameters.WaterBowlMaximumSecondsToWait;
    }
}

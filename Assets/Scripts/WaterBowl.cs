using UnityEngine;

public class WaterBowl : TimedObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        secondsOnScreen = GameParameters.WaterBowlSecondsOnScreen;
        base.Start();
    }
}

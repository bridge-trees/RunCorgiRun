using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BeerPlacer : TimedObjectPlacer
{
    public void Start()
    {
        MinimumSecondsToWait = GameParameters.BeerMinimumSecondsToWait;
        MaximumSecondsToWait = GameParameters.BeerMaximumSecondsToWait;
    }
}

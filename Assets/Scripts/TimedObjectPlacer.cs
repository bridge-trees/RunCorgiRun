using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject TimedObjectPrefab;
    
    private bool isActive = false;
    private bool isOkToCreate = false;
    
    private Coroutine countdownCoroutine;

    public float MinimumSecondsToWait = 1f;
    public float MaximumSecondsToWait = 3f;
    
    public void Update()
    {
        if (!isActive)
            return;
        
        if (isOkToCreate)
        {
            countdownCoroutine = StartCoroutine(CountdownUntilCreation());
        }
    }

    IEnumerator CountdownUntilCreation()
    {
        isOkToCreate = false;
        float secondsToWait = Random.Range(MinimumSecondsToWait, MaximumSecondsToWait);
        yield return new WaitForSeconds(secondsToWait);
        Place();
        isOkToCreate = true;
    }
    
    public virtual void Place()
    {
        Instantiate(TimedObjectPrefab, SpawnTools.RandomLocationWorldSpace(), Quaternion.identity);
    }

    public void StartPlacing()
    {
        isOkToCreate = true;
        isActive = true;
    }

    public void StopPlacing()
    {
        print("stopping placing");
        isActive = false;
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);
        
        CleanupPlacedObjects();
    }

    private void CleanupPlacedObjects()
    {
        print("cleaning up placed objects" + TimedObjectPrefab.tag);
        List<GameObject> placedObjects = GameObject.FindGameObjectsWithTag(TimedObjectPrefab.tag).ToList();
        for (int i = 0; i < placedObjects.Count; i++)
        {
            print("destroying " + TimedObjectPrefab.tag);
            Destroy(placedObjects[i]);
        }
    }
}

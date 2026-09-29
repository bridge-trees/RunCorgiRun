using System.Collections;
using UnityEngine;

public class Poop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CountdownUntilDeath());
    }

    IEnumerator CountdownUntilDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Enables invisible walls then deactivates them after a set time
public class InvisibleWallsOnAndOff : MonoBehaviour
{
    private List<Transform> invisibleWalls = new List<Transform>();

    private void Awake()
    {
        foreach (Transform wall in transform)
        {
            invisibleWalls.Add(wall);
        }
    }

    public void ActivateWallsForSetTime(float time)
    {
        foreach (Transform wall in invisibleWalls)
        {
            wall.gameObject.SetActive(true);
        }

        StartCoroutine(DeactivateWallsAfterTime(time));
    }

    IEnumerator DeactivateWallsAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (Transform wall in invisibleWalls)
        {
            wall.gameObject.SetActive(false);
        }
        
    }
}

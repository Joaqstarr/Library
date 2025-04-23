using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePuzzleSection : MonoBehaviour
{
    private List<Transform> m_Sections = new List<Transform>();

    private void Awake()
    {
        foreach (Transform t in transform)
        {
            m_Sections.Add(t);
        }
    }

    public void Raise()
    {
        foreach (Transform t in m_Sections)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject steamWall;
    [SerializeField]
    private GameObject pillar;

    public void disableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
        steamWall.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void enableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        steamWall.gameObject.GetComponent<BoxCollider>().enabled = true;
    }


}

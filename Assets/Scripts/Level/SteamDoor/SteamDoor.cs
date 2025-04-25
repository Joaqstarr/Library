using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        steamWall.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
    }
    public void enableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        steamWall.gameObject.GetComponent<BoxCollider>().enabled = true;
        steamWall.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
    }


}

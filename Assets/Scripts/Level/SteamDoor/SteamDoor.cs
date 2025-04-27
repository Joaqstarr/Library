using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class SteamDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject steamWall;
    [SerializeField]
    private GameObject pillar;

    [SerializeField] private GameObject vfx;

    void Start()
    {
        vfx.gameObject.GetComponent<VisualEffect>().SendEvent("OnPlay");
    }

    public void disableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
        steamWall.gameObject.GetComponent<BoxCollider>().enabled = false;
        steamWall.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        vfx.gameObject.GetComponent<VisualEffect>().SendEvent("OnStop");
    }
    public void enableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        steamWall.gameObject.GetComponent<BoxCollider>().enabled = true;
        steamWall.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
        vfx.gameObject.GetComponent<VisualEffect>().SendEvent("OnPlay");
    }


}

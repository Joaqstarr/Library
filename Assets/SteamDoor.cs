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
        steamWall.gameObject.GetComponent<MeshCollider>().enabled = false;
    }
    public void enableWall()
    {
        steamWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        steamWall.gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

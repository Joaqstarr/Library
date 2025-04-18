using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Events;

public class PressureSwitch : MonoBehaviour
{
    private MeshCollider _collider;
    private bool isSwitchActive = false;

    [SerializeField]
    private UnityEvent OnSwitchActivated;
    [SerializeField]
    private UnityEvent OnSwitchDeactivated;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<MeshCollider>();
    }

    private int objsInTrigger = 0;
    private void OnTriggerEnter(Collider other)
    {
        objsInTrigger++;

        if (!isSwitchActive && objsInTrigger == 1)
        {
            OnSwitchActivated.Invoke();
            isSwitchActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objsInTrigger--;
        //If there's no entitys in the collider and the switch is currently active
        if (isSwitchActive && objsInTrigger <= 0)
        {
            OnSwitchDeactivated.Invoke();
            isSwitchActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

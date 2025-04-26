using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Events;

public class PressureSwitch : MonoBehaviour
{
    private MeshCollider _collider;
    public bool isSwitchActive = false;
    private Animator _animator;
    
    

    [SerializeField]
    private UnityEvent OnSwitchActivated;
    [SerializeField]
    private UnityEvent OnSwitchDeactivated;

    [SerializeField]
    private bool syncedSwitches = false;
    [SerializeField]
    private GameObject[] SwitchGroup;


    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _animator = GetComponent<Animator>();
    }

    public int objsInTrigger { get; private set; } = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objs in " + gameObject.name + " is " + objsInTrigger);
        objsInTrigger++;
        
        if (!isSwitchActive && objsInTrigger == 1)
        {
            if (syncedSwitches == false) SingleObjectActivate();
            else {MultiObjectActivate();}
            _animator.SetTrigger("Pressed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("SwitchAttemptingToDeactivate" + isSwitchActive + objsInTrigger + gameObject.name);
        objsInTrigger--;
        //If there's no entitys in the collider and the switch is currently active
        if (isSwitchActive && objsInTrigger <= 0)
        {
            Debug.Log("MultiSwitchAttemptingToDeactivate");
            if (syncedSwitches == false) SingleObjectDeactivate();
            else {MultiObjectDeactivate();}
            _animator.SetTrigger("Unpressed");
        }
    }

    private void SingleObjectActivate()
    {
        OnSwitchActivated.Invoke();
        isSwitchActive = true;
    }

    private void SingleObjectDeactivate()
    {
        OnSwitchDeactivated.Invoke();
        isSwitchActive = false;
    }

    private void MultiObjectActivate()
    {
        bool allSwitchsArePressed = true;
        for (int i = 0; i < SwitchGroup.Length; i++)
        {
            if (SwitchGroup[i].gameObject.GetComponent<PressureSwitch>().objsInTrigger <= 0) allSwitchsArePressed = false;
        }

        if (allSwitchsArePressed)
        {
            OnSwitchActivated.Invoke();
            SetAllSwitches(true);
        }
    }
    private void MultiObjectDeactivate()
    {
        bool allSwitchsNotPressed = true;
        for (int i = 0; i < SwitchGroup.Length; i++)
        {
            if (SwitchGroup[i].gameObject.GetComponent<PressureSwitch>().objsInTrigger > 0) allSwitchsNotPressed = false;
        }

        if (allSwitchsNotPressed)
        {
            OnSwitchDeactivated.Invoke();
            SetAllSwitches(false);
        }
        
        
    }

    private void SetAllSwitches(bool state)
    {
        for (int i = 0; i < SwitchGroup.Length; i++)
        {
            SwitchGroup[i].gameObject.GetComponent<PressureSwitch>().isSwitchActive = state;
        }
    }
}

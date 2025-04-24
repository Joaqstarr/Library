using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePuzzleSection : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private bool _raisedTo1 = false;
    public void RaiseTo1()
    {
        if (!_raisedTo1) animator.SetTrigger("TrRaise1");
        _raisedTo1 = true;
    }
    
    private bool _raisedTo2 = false;
    public void RaiseTo2()
    {
        if (!_raisedTo2) animator.SetTrigger("TrRaise2");
        _raisedTo2 = true;
    }
}

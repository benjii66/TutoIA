using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Brain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;

    public bool IsValid => fsm;

    private void Start()
    {
        InitFSM();
    }

    void InitFSM()
    {
        if (!IsValid) return;


    }
}

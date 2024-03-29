﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIA_Brain : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] Animator fsm = null;
    [SerializeField] IA_Movement movement = null;
    [SerializeField] IA_Detection detection = null;
    [SerializeField] IA_WaypointSystem pattern = null;
    [SerializeField] IA_Heal heal = null;
    [SerializeField] IA_Attack attack = null;
    [SerializeField] P_Player player = null;

    [Header("Parameters")]

    [SerializeField] string chaseParameter = "Chase_Target", patternParameter = "Follow_Pattern", waitParameter = "Wait", isHealing = "Is_Healing", isHealed = "Is_Healed", isHitting = "Is_Hitting", isDead = "Is_Dead", IsOnRange = "Is_Range";

    [Header("States")]
    [SerializeField] IA_ChaseState chaseState = null;
    [SerializeField] IA_PatternState patternState = null;
    [SerializeField] IA_WaitState waitState = null;
    [SerializeField] IA_HealState healState = null;
    [SerializeField] IA_AttackState attackState = null;


    public bool IsValid => fsm && movement && detection && pattern && chaseState && patternState && waitState && attackState && healState;


    public Animator FSM => fsm;
    public IA_Movement Movement => movement;
    public IA_Detection Detection => detection;
    public IA_WaypointSystem Pattern => pattern;
    public IA_Heal Heal => heal;
    public IA_Attack Attack => attack;



    private void Start()
    {
        InitFSM();
    }

    void InitFSM()
    {
        movement = GetComponent<IA_Movement>();
        detection = GetComponent<IA_Detection>();
        pattern = GetComponent<IA_WaypointSystem>();
        heal = GetComponent<IA_Heal>();
        attack = GetComponent<IA_Attack>();
        player = GetComponent<P_Player>();
        if (!fsm) return;
        IA_State[] _states = fsm.GetBehaviours<IA_State>();
        for (int i = 0; i < _states.Length; i++)
            _states[i].CInitState(this);


        chaseState = fsm.GetBehaviour<IA_ChaseState>();
        patternState = fsm.GetBehaviour<IA_PatternState>();
        waitState = fsm.GetBehaviour<IA_WaitState>();
        healState = fsm.GetBehaviour<IA_HealState>();
        attackState = fsm.GetBehaviour<IA_AttackState>();

        if (!IsValid) return;

        #region Event Attachement

        detection.OnTargetDetected += (position) =>
        {
            movement.SetTarget(position);
            fsm.SetBool(chaseParameter, true);
            fsm.SetBool(patternParameter, false);
            fsm.SetBool(IsOnRange, true);
            fsm.SetBool(isHitting, true);

        };

        detection.OnTargetLost += () =>
        {
            fsm.SetBool(chaseParameter, false);
            fsm.SetBool(patternParameter, true);
            fsm.SetBool(IsOnRange, false);
            fsm.SetBool(isHitting, false);
            if (heal.health != 100)
                fsm.SetBool(isHealing, true);
        };

        movement.OnPositionReached += () =>
        {
            fsm.SetBool(waitParameter, true);
        };

        if (heal.health != 100)
            heal.OnHeal += () =>
            {
                fsm.SetBool(isHealing, true);
                fsm.SetBool(isHealed, false);
                fsm.SetBool(IsOnRange, false);

            };

      

        #endregion
    }

    private void Update() => UpdateBrain();

    public void UpdateBrain()
    {
        if (!IsValid) return;
        detection.UpdateDetection();
    }

}

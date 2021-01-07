using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Brain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] IA_Movement movement = null;
    [SerializeField] IA_Detection detection = null;
    [SerializeField] IA_WaypointSystem pattern = null;

    [SerializeField] string chaseParameter = "Chase_Target",patternParameter = "Follow_Pattern", waitParameter = "Wait";

    [SerializeField] IA_ChaseState chaseState = null;
    [SerializeField] IA_PatternState patternState = null;
    [SerializeField] IA_WaitState waitState = null;


    public bool IsValid => fsm && movement && detection && pattern && chaseState && patternState && waitState;


    public Animator FSM => fsm;
    public IA_Movement Movement => movement;
    public IA_Detection Detection => detection;
    public IA_WaypointSystem Pattern => pattern;



    private void Start()
    {
        InitFSM();
    }

    void InitFSM()
    {
        movement = GetComponent<IA_Movement>();
        detection = GetComponent<IA_Detection>();
        pattern = GetComponent<IA_WaypointSystem>();
        if (!fsm) return;
        IA_State[] _states = fsm.GetBehaviours<IA_State>();
        for (int i = 0; i < _states.Length; i++)
            _states[i].InitState(this);
        chaseState = fsm.GetBehaviour<IA_ChaseState>();
        patternState = fsm.GetBehaviour<IA_PatternState>();
        waitState = fsm.GetBehaviour<IA_WaitState>();
        if (!IsValid) return;
        chaseState.InitState(this);
        patternState.InitState(this);
        waitState.InitState(this);
        detection.OnTargetDetected += (position) =>
        {
            movement.SetTarget(position);
            fsm.SetBool(chaseParameter, true);
            fsm.SetBool(patternParameter, false);
        };

        detection.OnTargetLost += () =>
        {
            fsm.SetBool(chaseParameter, false);
            fsm.SetBool(patternParameter, true);
        };

        movement.OnPositionReached += () =>
        {
            fsm.SetBool(waitParameter, true);
            fsm.SetBool(chaseParameter, false);
            fsm.SetBool(waitParameter, true);
        };
    }

    private void Update() => UpdateBrain();

    public void UpdateBrain()
    {
        if (!IsValid) return;
        detection.UpdateDetection();
    }

}

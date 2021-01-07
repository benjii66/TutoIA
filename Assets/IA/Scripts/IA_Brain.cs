using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Brain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] IA_Movement movement = null;
    [SerializeField] IA_Detection detection = null;
    [SerializeField] IA_WaypointSystem pattern = null;
    [SerializeField] IA_Heal heal = null;
    [SerializeField] IA_Attack attack = null;
    

    [SerializeField] string chaseParameter = "Chase_Target", patternParameter = "Follow_Pattern", waitParameter = "Wait", isHealing = "Is_Healing", isHealed = "Is_Healed", isHitting = "Is_Hitting",isDead = "Is_Dead", IsOnRange = "Is_Range";

    [SerializeField] IA_ChaseState chaseState = null;
    [SerializeField] IA_PatternState patternState = null;
    [SerializeField] IA_WaitState waitState = null;
    [SerializeField] IA_HealState healState = null;
    [SerializeField] IA_AttackState attackState = null;


    public bool IsValid => fsm && movement && detection && pattern && chaseState && patternState && waitState  && attackState;//&& healState


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
        if (!fsm) return;
        IA_State[] _states = fsm.GetBehaviours<IA_State>();
        for (int i = 0; i < _states.Length; i++)
            _states[i].InitState(this);


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

	 };

		detection.OnTargetLost += () =>
		{
			fsm.SetBool(chaseParameter, false);
			fsm.SetBool(patternParameter, true);
		};

		movement.OnPositionReached += () =>
		{
			fsm.SetBool(waitParameter, true);
		};


		heal.OnHeal += () =>
		{
			fsm.SetBool(isHealing, true);
            fsm.SetBool(isHealed, false);

        };

        heal.OnFullHeal += () =>
        {
            fsm.SetBool(isHealed, true);
            fsm.SetBool(isHealing, false);
        };

		attack.OnHit += () =>
		{
            fsm.SetBool(chaseParameter, true);
			fsm.SetBool(isHealing, false);
			fsm.SetBool(isHitting, true);
		};

        attack.OnRange += () =>
        {
            fsm.SetBool(isDead, false);
            fsm.SetBool(IsOnRange, true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_HealState : IA_State
{
    public override void InitState(IA_Brain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
     
            brain.Heal.Heal();
            brain.FSM.SetFloat("Wait_Timer", Random.Range(.1f, 1));
            brain.FSM.SetBool("Is_Healing", true);
            brain.FSM.SetBool("Is_Healed", false);

        };
        OnExit += () =>
        {
            brain.FSM.SetBool("Is_Healed", true);
            brain.FSM.SetBool("Is_Healing", false);
        };
    }


}

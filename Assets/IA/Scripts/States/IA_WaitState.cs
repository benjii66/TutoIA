using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_WaitState : IA_State
{
    public override void InitState(IA_Brain _brain)
    {
        base.InitState(_brain);
        OnEnter += () => brain.FSM.SetFloat("Wait_Timer", Random.Range(.1f, 1));
        OnExit += () => brain.FSM.SetBool("Wait", false);
    }

}

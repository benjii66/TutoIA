using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_ChaseState : IA_State
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        OnUpdate += brain.Movement.MoveTo;
    }
}

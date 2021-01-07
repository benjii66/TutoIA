using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_PatternState : IA_State
{
    public override void InitState(IA_Brain _brain)
    {
        base.InitState(_brain);
        OnEnter += () => brain.Movement.SetTarget(brain.Pattern.PickPoint());
        OnUpdate += () => brain.Movement.MoveTo();
    }
}

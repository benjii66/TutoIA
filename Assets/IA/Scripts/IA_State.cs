
using System;
using UnityEngine;

public abstract class IA_State : StateMachineBehaviour
{
    public event Action OnEnter = null;
    public event Action OnUpdate= null;
    public event Action OnExit = null;
    protected IA_Brain brain = null;
    protected CIA_Brain cBrain = null;

    public virtual void InitState(IA_Brain _brain) => brain = _brain;
    public virtual void CInitState(CIA_Brain _brain) => cBrain = _brain;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();



}

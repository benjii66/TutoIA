
using System;
using UnityEngine;

public abstract class IA_State : StateMachineBehaviour
{
    public event Action OnStart = null;
    public event Action OnUpdate= null;
    public event Action OnExit = null;
    protected IA_Brain brain = null;

    public IA_Brain InitBrain(IA_Brain _brain) => brain = _brain;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnStart?.Invoke();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();



}

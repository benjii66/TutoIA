using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CIA_FightSystem : MonoBehaviour
{
    public event Action<float> onAttack = null;
    public event Action OnAttackRange = null;
    public event Action OnAttackLost = null;



    [SerializeField, Range(0.1f, 2)] float attackRange = .1f;
    [SerializeField, Range(0.1f, 10)] float attackRate = .1f;
    [SerializeField, Range(0, 100)] int fightDamages = 10;


    float attackTimer = 0;

    ITarget attackTarget = null;


    public void SetAttackTarget(GUITargetAttribute _target) => attackTarget = _target;
    public float AttackRate => attackRate;


    public void UpdateFightSystem()
    {
        if (isAtAttackRange())
            OnAttackRange?.Invoke();
        else OnAttackLost?.Invoke();
    }

    public void AttackTarget()
    {
        if (attackTarget != null && attackTarget.IsDead || !isAtAttackRange())
        {
            onAttack?.Invoke(false, 0);
            return;
        }
        attackTimer += Time.deltaTime;
        if (attackTimer > attackRate)
        {
            onAttack?.Invoke(true, attackRate);
            attackTarget?.SetDamage(fightDamages);
            attackTimer = 0;
        }

    }

    bool isAtAttackRange()
    {
        if (attackTarget == null) return false;
        return Vector3.Distance(transform.position, attackTarget.TargetPosition) < attackRange;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


public interface ITarget : IStats
{
    Vector3 TargetPosition { get; }

}

public interface IStats
{
    event Action<bool> OnNeedHeal;
    event Action OnDie;
    event Action<float> OnLife;

    bool IsDead { get; }
    bool NeedHeal { get; }
    float Life { get; }
    void SetDamage(float _dmg);
    void AddLife(float _life);

}
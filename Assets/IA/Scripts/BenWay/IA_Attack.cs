using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA_Attack : MonoBehaviour
{
    public event Action OnHit = null, OnRange = null;

    [SerializeField] Animator enemyAnim = null;
    [SerializeField, Range(0, 100)] int damage = 5;
    [SerializeField, Range(0, 50)] float hitRange = 0;
    [SerializeField] P_Player player = null;


    public Vector3 targetPos => player.transform.position;

    public bool IsOnRange => Vector3.Distance(transform.position, targetPos) >= hitRange;


    public void Hit()
    {
        if (player.Health > 0)
        {
            enemyAnim.Play("Attack");
            player.SetHealth(player.health -= damage);
            OnRange?.Invoke();
        }

    }


    public void DistanceRange()
    {
        float _distance = Vector3.Distance(targetPos, transform.position);

        if (IsOnRange)
        {      
            OnHit?.Invoke();
            OnHit += () => Hit();
            if (player.Health > 0)
                OnHit -= () => Hit();

        }
    }


    private void OnDestroy()
    {
        OnHit = null;
        OnRange = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}

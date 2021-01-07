using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA_Movement : MonoBehaviour
{
    public event Action OnPositionReached = null;
    [SerializeField]Vector3 targetPosition = Vector3.zero;
    [SerializeField,Range(0,10)]float atPosRange = 2;

    public bool IsAtPos => Vector3.Distance(targetPosition, transform.position) < atPosRange;


    public void SetTarget(Vector3 _target) => targetPosition = _target; 

    public void MoveTo()
    {
        if(IsAtPos)
        {
            OnPositionReached?.Invoke();
            return; 
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, .1f);
    }
}

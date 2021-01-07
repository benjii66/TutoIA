using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA_Detection : MonoBehaviour
{
    public event Action<Vector3> OnTargetDetected = null;

    //ajouter un vector3 pour une phase recherche potientielle
    public event Action OnTargetLost = null;


    [SerializeField] Transform target = null;
    [SerializeField, Range(1,10)] int detectionRange = 5;


    public bool IsDetected { get; private set; } = false;

    //intéressant ça ! 
    public bool IsAtRange
    {
        get
        {
            if (!IsValid) return false;
                return Vector3.Distance(transform.position, target.position) < detectionRange;
        }
    }

    public bool IsValid => target;

    //
    private void Awake()
    {
        OnTargetDetected += (point) => IsDetected = true;
        OnTargetLost += ()=> IsDetected = false;
    }


    public void UpdateDetection()
    {
        if (!IsValid) return;
        if (IsAtRange) OnTargetDetected?.Invoke(target.position);
        else
        {
            OnTargetLost?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnTargetDetected = null;
        OnTargetLost = null;
    }


    private void OnDrawGizmos()
    {
        if(IsDetected && IsValid)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.position);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

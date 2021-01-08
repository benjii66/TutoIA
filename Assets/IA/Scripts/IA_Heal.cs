using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA_Heal : MonoBehaviour
{
    public event Action OnHeal = null;
    public event Action OnFullHeal = null;
    public event Action<Vector3> OnRange = null;
    public event Action OnTranquilidad = null;

    [SerializeField] float healDuration = 0.01f;
    [SerializeField] public int health = 100;
    [SerializeField] int healZone = 4;
    [SerializeField] float durationParticle = 10.0f;
    [SerializeField] Transform target = null;
    [SerializeField] GameObject healParticle = null;


    public bool TargetAtRange
    {
        get
        {
            if (!IsValid) return false;
            return Vector3.Distance(transform.position, target.position) < healZone;
        }
    }

    public bool IsFullLife
    {
        get
        {
            if (health == 100) return true;
            return false;
        }
    }
    public bool IsValid => target;
    public int Health => health;

    public bool IsOnZone { get; private set; } = false;


    private void Awake()
    {
        OnRange += (point) => IsOnZone = true;
        OnTranquilidad += () => IsOnZone = false;

    }

    private void Update()
    {
       
    }

    public void Heal()
    {
        if (!TargetAtRange && health != 100)
        {
            Debug.Log("Il heal");
            health += (int)(healDuration * Time.smoothDeltaTime);

            OnHeal?.Invoke();
            OnHeal += () => InstantiateFXEffect(healParticle, Vector3.zero, .1f);

            if (health <= 100)
            {
               OnHeal -= () =>InstantiateFXEffect(healParticle, Vector3.zero, .1f);
                health = 100;
                OnFullHeal?.Invoke();
            }
        }
    }


    void InstantiateFXEffect(GameObject _fx, Vector3 _position, float _sizeFX = 1)
    {
        GameObject _effect = Instantiate(_fx, _position, Quaternion.identity);
        _effect.transform.localScale *= _sizeFX;
        Destroy(_effect, durationParticle);
    }


    private void OnDestroy()
    {
        OnHeal = null;
        OnFullHeal = null;
        OnRange = null;
        OnTranquilidad = null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, healZone);
    }
}

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


	[SerializeField] int health = 100;
	[SerializeField] int healZone = 4;
	[SerializeField] Transform target = null;


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
		Heal();
		OnHeal?.Invoke();
	}

	public void Heal()
	{
		if (!TargetAtRange)
		{
			health += health * (int)Time.deltaTime;
			Debug.Log("dude wtf");
			//joue l'effet
			//joue l'anim
			if (health == 100)
				OnFullHeal?.Invoke();
		}
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_AttackState : IA_State
{
	public override void InitState(IA_Brain _brain)
	{
		base.InitState(_brain);
		OnEnter += () =>
		{

			brain.Attack.DistanceRange();
			brain.FSM.SetBool("Is_Hitting", true);
			brain.FSM.SetBool("Is_Kill", false);
		};

		if (!brain.Attack.IsOnRange)
			OnExit += () =>
			{
				brain.FSM.SetBool("Is_Hitting", false);
				brain.FSM.SetBool("Is_Kill", true);
			};
	}


}
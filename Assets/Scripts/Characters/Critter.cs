using System;
using UnityEngine;

public class Critter : MonoBehaviour
{
	[HideInInspector] public bool isPlayerInAlertZone;
	[HideInInspector] public bool isPlayerInAttackZone;
	[SerializeField] EnemyHealth HPCanvas;
	public Damageable currentTarget; //The StateMachine evaluates its health when needed

	public void OnAlertTriggerChange(bool entered, GameObject who)
	{
		isPlayerInAlertZone = entered;

		if (entered && who.TryGetComponent(out Damageable d))
		{
			HPCanvas.OnAttack();

			currentTarget = d;
			currentTarget.OnDie += OnTargetDead;
		}
		else
		{
			HPCanvas.OnIdle();
			currentTarget = null;
		}
	}

	public void OnAttackTriggerChange(bool entered, GameObject who)
	{
		isPlayerInAttackZone = entered;

		//No need to set the target. If we did, we would get currentTarget to null even if
		//a target exited the Attack zone (inner) but stayed in the Alert zone (outer).
	}

	private void OnTargetDead()
	{
		currentTarget = null;
		HPCanvas.OnPlayerDead();
		isPlayerInAlertZone = false;
		isPlayerInAttackZone = false;
	}
}

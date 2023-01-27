using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICircleBar : MonoBehaviour
{
	[SerializeField] Damageable damageableScript;
	[SerializeField] Image cirlceHealthUI;
	[SerializeField] Image barHealthUI;

	public int maxHealth;
	public int currentHealth;

	public float circlePercentageBar = 1/3f;

	private void Awake()
	{
		if (damageableScript == null)
		{
			maxHealth = 20;
			currentHealth = maxHealth;
		}
		else
		{
			maxHealth = damageableScript._healthConfigSO.MaxHealth;
		}
		//FindPlayer();
	}

	private void Update()
	{
		if (damageableScript != null)
		{
			currentHealth = damageableScript._currentHealth;
		}

		CircleFillAmount();
		BarFillAmount();
	}

	private void BarFillAmount()
	{
		float circleAmount = circlePercentageBar * maxHealth;

		float barHealth = currentHealth - circleAmount;
		float barTotalHealth = maxHealth - circleAmount;

		float barFill = barHealth / barTotalHealth;

		barFill = Mathf.Clamp(barFill, 0, 1);

		barHealthUI.fillAmount = barFill;
	}

	private void CircleFillAmount()
	{
		float circleFillPercentage = (maxHealth * circlePercentageBar);

		if (currentHealth < circleFillPercentage)
		{
			float circleFill = (currentHealth / circleFillPercentage);

			circleFill = Mathf.Clamp(circleFill, 0, circleFillPercentage);


			cirlceHealthUI.fillAmount = circleFill;
		}
		else
		{
			cirlceHealthUI.fillAmount = 1;
		}
	}

	private IEnumerator FindPlayer()
	{
		yield return new WaitForSeconds(1);

		damageableScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();
	}
}
